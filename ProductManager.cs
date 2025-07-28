using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Final
{
    internal class ProductManager
    {
        private List<Product> Products;
        private readonly IDataStorage storage;
        private readonly string ProductFile;
        private int nextProductId;

        public ProductManager(List<Product> products, IDataStorage storage, string productFile, int nextProductId)
        {
            Products = products;
            this.storage = storage;
            ProductFile = productFile;
            this.nextProductId = nextProductId;
        }

        public void AddProduct()
        {
            Console.Write("Name: "); 
            var name = Console.ReadLine();
            Console.Write("Description: "); 
            var description = Console.ReadLine();
            Console.Write("Price: "); 
            var price = double.Parse(Console.ReadLine());
            Console.Write("Stock: "); var stock = int.Parse(Console.ReadLine());
            Products.Add(new Product(nextProductId++, name, description, price, stock));
            storage.SaveProducts(Products, ProductFile);
            Console.WriteLine("Product added.");
        }

        public void EditProduct()
        {
            Console.Write("Product ID: "); 
            var id = int.Parse(Console.ReadLine());
            var prod = Products.FirstOrDefault(p => p.ID == id);
            if (prod == null) 
            { 
                Console.WriteLine("Not found."); 
                return; 
            }
            Console.Write("New name (enter for skip): "); 
            var name = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(name)) 
                prod.Name = name;
            Console.Write("New description (enter for skip): "); 
            var description = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(description)) 
                prod.Description = description;
            
            Console.Write("New price (enter for skip): "); 
            var price = Console.ReadLine();
            
            if (double.TryParse(price, out var p)) 
                prod.Price = p;
            Console.Write("New stock (enter for skip): "); 
            var stock = Console.ReadLine();
            
            if (int.TryParse(stock, out var s)) 
                prod.Stock = s;
            storage.SaveProducts(Products, ProductFile);
            Console.WriteLine("Product updated.");
        }

        public void DeleteProduct()
        {
            Console.Write("Product ID: "); 
            var id = int.Parse(Console.ReadLine());
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

    }
}
