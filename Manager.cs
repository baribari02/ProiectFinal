using System;
using System.Collections.Generic;
using System.Linq;

public class Manager
{
    // DATA
    public List<Product> Products { get; private set; } = new();
    public List<User> Users { get; private set; } = new();
    public List<Order> Orders { get; private set; } = new();
    public Cart CurrentCart { get; private set; }
    public User LoggedUser { get; private set; }
    int nextProductId = 1;
    int nextUserId = 1;
    int nextOrderId = 1;

    // SERVICES
    readonly IDataStorage storage = new FileDataStorage();
    readonly IFormChecking formChecker = new SimpleFormChecker();

    // FILES
    const string ProductFile = "products.txt";
    const string UserFile = "users.txt";
    const string OrderFile = "orders.txt";

    public Manager()
    {
        Products = storage.LoadProducts(ProductFile);
        Users = storage.LoadUsers(UserFile);
        Orders = storage.LoadOrders(OrderFile);
        if (!Products.Any()) SeedExampleData();
        if (!Users.Any()) Users.Add(new User(nextUserId++, "admin", "admin@example.com", "admin", "admin123"));
        nextProductId = Products.Any() ? Products.Max(p => p.ID) + 1 : 1;
        nextUserId = Users.Any() ? Users.Max(u => u.ID) + 1 : 1;
        nextOrderId = Orders.Any() ? Orders.Max(o => o.ID) + 1 : 1;
        // Fix references for Orders
        foreach (var order in Orders)
        {
            order.User = Users.FirstOrDefault(u => u.ID == (order.User?.ID ?? -1));
            // Products in orders: Not loaded from file by default, need to parse order line if you want to persist
        }
    }

    void SeedExampleData()
    {
        Products.Add(new Product(nextProductId++, "Laptop", "Gaming Laptop", 1200, 10));
        Products.Add(new Product(nextProductId++, "Mouse", "Wireless Mouse", 20, 100));
        Products.Add(new Product(nextProductId++, "Keyboard", "Mechanical Keyboard", 50, 50));
    }

    // USER MANAGEMENT
    public void Register()
    {
        Console.Write("Name: "); var name = Console.ReadLine();
        Console.Write("Email: "); var email = Console.ReadLine();
        if (!formChecker.ValidateEmail(email)) { Console.WriteLine("Invalid email."); return; }
        if (Users.Any(u => u.Email == email)) { Console.WriteLine("Email already used."); return; }
        Console.Write("Password: "); var pwd = Console.ReadLine();
        if (!formChecker.ValidatePassword(pwd)) { Console.WriteLine("Password too short."); return; }
        var user = new User(nextUserId++, name, email, "customer", pwd);
        Users.Add(user);
        storage.SaveUsers(Users, UserFile);
        Console.WriteLine("Registered successfully.");
    }
    public void Login()
    {
        Console.Write("Email: "); var email = Console.ReadLine();
        Console.Write("Password: "); var pwd = Console.ReadLine();
        LoggedUser = Users.FirstOrDefault(u => u.Email == email && u.Password == pwd);
        if (LoggedUser == null) Console.WriteLine("Login failed.");
        else { Console.WriteLine($"Welcome, {LoggedUser.Name}!"); CurrentCart = new Cart(LoggedUser); }
    }
    public void ListUsers() => Users.ForEach(u => Console.WriteLine(u));

    // PRODUCT MANAGEMENT
    public void AddProduct()
    {
        Console.Write("Name: "); var name = Console.ReadLine();
        Console.Write("Description: "); var desc = Console.ReadLine();
        Console.Write("Price: "); var price = decimal.Parse(Console.ReadLine());
        Console.Write("Stock: "); var stock = int.Parse(Console.ReadLine());
        Products.Add(new Product(nextProductId++, name, desc, price, stock));
        storage.SaveProducts(Products, ProductFile);
        Console.WriteLine("Product added.");
    }
    public void EditProduct()
    {
        Console.Write("Product ID: "); var id = int.Parse(Console.ReadLine());
        var prod = Products.FirstOrDefault(p => p.ID == id);
        if (prod == null) { Console.WriteLine("Not found."); return; }
        Console.Write("New name (enter for skip): "); var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name)) prod.Name = name;
        Console.Write("New desc (enter for skip): "); var desc = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(desc)) prod.Description = desc;
        Console.Write("New price (enter for skip): "); var price = Console.ReadLine();
        if (decimal.TryParse(price, out var p)) prod.Price = p;
        Console.Write("New stock (enter for skip): "); var stock = Console.ReadLine();
        if (int.TryParse(stock, out var s)) prod.Stock = s;
        storage.SaveProducts(Products, ProductFile);
        Console.WriteLine("Product updated.");
    }
    public void DeleteProduct()
    {
        Console.Write("Product ID: "); var id = int.Parse(Console.ReadLine());
        Products.RemoveAll(p => p.ID == id);
        storage.SaveProducts(Products, ProductFile);
        Console.WriteLine("Deleted.");
    }
    public void ListProducts() => Products.ForEach(p => Console.WriteLine(p));
    public void SearchProducts()
    {
        Console.Write("Name contains: "); var term = Console.ReadLine();
        var found = Products.Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        if (!found.Any()) Console.WriteLine("No products found.");
        else found.ForEach(p => Console.WriteLine(p));
    }
    public void FilterProductsByPrice()
    {
        Console.Write("Min price: "); var min = decimal.Parse(Console.ReadLine());
        Console.Write("Max price: "); var max = decimal.Parse(Console.ReadLine());
        var found = Products.Where(p => p.Price >= min && p.Price <= max).ToList();
        if (!found.Any()) Console.WriteLine("No products found.");
        else found.ForEach(p => Console.WriteLine(p));
    }

    // CART & CHECKOUT
    public void AddToCart()
    {
        Console.Write("Product ID: "); var id = int.Parse(Console.ReadLine());
        var prod = Products.FirstOrDefault(p => p.ID == id);
        if (prod == null) { Console.WriteLine("Not found."); return; }
        Console.Write("Quantity: "); var qty = int.Parse(Console.ReadLine());
        if (qty > prod.Stock) { Console.WriteLine("Insufficient stock."); return; }
        CurrentCart.AddItem(prod, qty);
        Console.WriteLine("Added to cart.");
    }
    public void RemoveFromCart()
    {
        Console.Write("Product ID to remove: "); var id = int.Parse(Console.ReadLine());
        CurrentCart.RemoveItem(id);
        Console.WriteLine("Removed from cart.");
    }
    public void ViewCart() => Console.WriteLine(CurrentCart);

    public void Checkout()
    {
        try
        {
            foreach (var item in CurrentCart.Items)
            {
                var prod = Products.First(p => p.ID == item.Product.ID);
                if (prod.Stock < item.Quantity) throw new Exception($"Out of stock: {prod.Name}");
            }
            foreach (var item in CurrentCart.Items)
            {
                var prod = Products.First(p => p.ID == item.Product.ID);
                prod.Stock -= item.Quantity;
            }
            Console.Write("Shipping address: "); var addr = Console.ReadLine();
            var order = new Order(nextOrderId++, LoggedUser, new List<CartItem>(CurrentCart.Items), addr);
            Orders.Add(order);
            storage.SaveOrders(Orders, OrderFile);
            storage.SaveProducts(Products, ProductFile);
            CurrentCart.Items.Clear();
            Console.WriteLine("Checkout successful. Order created:\n" + order);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Checkout failed: " + ex.Message);
        }
    }

    // REPORTING
    public void OrderSummary()
    {
        var totalSales = Orders.Sum(o => o.TotalPrice);
        Console.WriteLine($"Total sales: {totalSales:C}");
        var top = Orders.SelectMany(o => o.Products)
            .GroupBy(i => i.Product.Name)
            .OrderByDescending(g => g.Sum(i => i.Quantity))
            .Take(3);
        Console.WriteLine("Top products:");
        foreach (var g in top)
            Console.WriteLine($"{g.Key}: {g.Sum(i => i.Quantity)} sold.");
    }

    public void SaveAll()
    {
        storage.SaveProducts(Products, ProductFile);
        storage.SaveUsers(Users, UserFile);
        storage.SaveOrders(Orders, OrderFile);
    }

    public bool IsAdmin() => LoggedUser != null && LoggedUser.Role == "admin";
    public bool IsLogged() => LoggedUser != null;
    public void Error() => Console.WriteLine("You must be logged in (and admin for this action).");
}