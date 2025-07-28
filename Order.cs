using System;
using System.Collections.Generic;
using System.Linq;

public class Order
{
    public int ID { get; set; }
    public User User { get; set; }
    public List<CartItem> Products { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Timestamp { get; set; }
    public string ShippingAddress { get; set; }

    public Order(int id, User user, List<CartItem> items, string address)
    {
        ID = id; User = user; Products = items; TotalPrice = items.Sum(i => i.TotalPrice);
        Timestamp = DateTime.Now; ShippingAddress = address;
    }
    public Order() { }
    public override string ToString() => $"Order {ID} by {User?.Name} on {Timestamp:G}\nShip to: {ShippingAddress}\n" +
        string.Join("\n", Products.Select(i => i.ToString())) + $"\nTotal: {TotalPrice:C}";
}