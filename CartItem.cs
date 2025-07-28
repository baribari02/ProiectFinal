public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public CartItem(Product product, int qty)
    {
        Product = product; Quantity = qty;
    }
    public decimal TotalPrice => Product.Price * Quantity;
    public override string ToString() => $"{Product.Name} x{Quantity} = {TotalPrice:C}";
}