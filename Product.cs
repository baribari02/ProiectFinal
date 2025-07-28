using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Final
{
   internal class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, string description, double price, int stock)
        {
            ID = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
        public override string ToString() => $"ID:{ID}\nName:{Name}\nDescription:{Description}\nPrice:{Price:C}\nStock: {Stock}";
    }
}
