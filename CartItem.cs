using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj1._0
{
    internal class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int q)
        {
            Product = product; Quantity = q;
        }
        public double TotalPrice => Product.Price * Quantity;
        public override string ToString() => $"{Product.Name} x{Quantity} = {TotalPrice:C}";
    }
}
