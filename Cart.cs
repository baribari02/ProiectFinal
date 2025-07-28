using System.Collections.Generic;
using System.Linq;

public class Cart
{
    public List<CartItem> Items { get; set; } = new();
    public User User { get; set; }
    public decimal Total => Items.Sum(i => i.TotalPrice);

    public Cart(User user) { User = user; }
    public void AddItem(Product product, int qty)
    {
        var item = Items.FirstOrDefault(i => i.Product.ID == product.ID);
        if (item == null) Items.Add(new CartItem(product, qty));
        else item.Quantity += qty;
    }
    public void RemoveItem(int productId)
    {
        var item = Items.FirstOrDefault(i => i.Product.ID == productId);
        if (item != null) Items.Remove(item);
    }
    public override string ToString() => string.Join("\n", Items.Select(i => i.ToString())) + $"\nTotal: {Total:C}";
}