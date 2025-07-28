using System;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Product(int id, string name, string desc, decimal price, int stock)
    {
        ID = id; Name = name; Description = desc; Price = price; Stock = stock;
    }
    public override string ToString() => $"[{ID}] {Name} - {Description}, Price: {Price:C}, Stock: {Stock}";
}