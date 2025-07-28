using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proiect_Final
{
    internal class FileDataStorage : IDataStorage
    {
        public void SaveProducts(List<Product> products, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var product in products)
                {
                    
                    sw.WriteLine($"{product.ID}|{product.Name}|{product.Description}|{product.Price}|{product.Stock}");
                }
            }
        }

        public List<Product> LoadProducts(string filePath)
        {
            List<Product> list = new List<Product>();
            if (!File.Exists(filePath))
                return list;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 5 &&
                    int.TryParse(parts[0], out int id) &&
                    double.TryParse(parts[3], out double price) &&
                    int.TryParse(parts[4], out int stock))
                {
                    list.Add(new Product(
                        id,
                        parts[1],
                        parts[2],
                        price,
                        stock
                    ));
                }
            }
            return list;
        }
    }
}