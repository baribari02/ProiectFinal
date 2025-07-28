using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class FileDataStorage : IDataStorage
{
    public void SaveProducts(List<Product> products, string filePath)
    {
        File.WriteAllLines(filePath, products.Select(p =>
            $"{p.ID}|{p.Name}|{p.Description}|{p.Price}|{p.Stock}"));
    }
    public List<Product> LoadProducts(string filePath)
    {
        if (!File.Exists(filePath)) return new();
        return File.ReadAllLines(filePath).Select(line =>
        {
            var s = line.Split('|');
            return new Product(int.Parse(s[0]), s[1], s[2], decimal.Parse(s[3]), int.Parse(s[4]));
        }).ToList();
    }

    public void SaveUsers(List<User> users, string filePath)
    {
        File.WriteAllLines(filePath, users.Select(u =>
            $"{u.ID}|{u.Name}|{u.Email}|{u.Role}|{u.Password}"));
    }
    public List<User> LoadUsers(string filePath)
    {
        if (!File.Exists(filePath)) return new();
        return File.ReadAllLines(filePath).Select(line =>
        {
            var s = line.Split('|');
            return new User(int.Parse(s[0]), s[1], s[2], s[3], s[4]);
        }).ToList();
    }

    public void SaveOrders(List<Order> orders, string filePath)
    {
        File.WriteAllLines(filePath, orders.Select(o =>
            $"{o.ID}|{o.User.ID}|{o.ShippingAddress}|{o.Timestamp:O}|" +
            string.Join(',', o.Products.Select(i => $"{i.Product.ID}:{i.Quantity}")) + $"|{o.TotalPrice}"));
    }
    public List<Order> LoadOrders(string filePath)
    {
        if (!File.Exists(filePath)) return new();
        // NOTE: Product and User references must be fixed externally after loading!
        var lines = File.ReadAllLines(filePath);
        var orders = new List<Order>();
        foreach (var line in lines)
        {
            var s = line.Split('|');
            orders.Add(new Order
            {
                ID = int.Parse(s[0]),
                User = null, // to be set by Manager after loading
                ShippingAddress = s[2],
                Timestamp = DateTime.Parse(s[3]),
                Products = new List<CartItem>(), // to be set by Manager after loading
                TotalPrice = decimal.Parse(s[5])
            });
        }
        return orders;
    }
}