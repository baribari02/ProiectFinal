using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj1._0
{
    internal class Cart
    {
        public List<CartItem> Items { get; set; } = new();
        public User User { get; set; }
        public double Total => Items.Sum(i => i.TotalPrice);

        public Cart(User user) { User = user; }
        public void AddItem(Product product, int q)
        {
            var item = Items.FirstOrDefault(i => i.Product.ID == product.ID);
            if (item == null) Items.Add(new CartItem(product, q));
            else item.Quantity += q;
        }
        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.Product.ID == productId);
            if (item != null) Items.Remove(item);
        }
        public override string ToString() => string.Join("\n", Items.Select(i => i.ToString())) + $"\nTotal: {Total:C}";
    }
}

