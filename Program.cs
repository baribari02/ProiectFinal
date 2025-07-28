using System;
using System.Collections.Generic;

namespace Proiect_Final
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IDataStorage storage = new FileDataStorage();

            List<Product> products = storage.LoadProducts("products.txt");

            int nextProductId = products.Count > 0 ? products[^1].ID + 1 : 1;

            ProductManager productManager = new ProductManager(products, storage, "products.txt", nextProductId);

            while (true)
            {
                Console.WriteLine("\n--- Menu Product ---");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Modify Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. List Product");
                Console.WriteLine("5. Search by name");
                Console.WriteLine("0. Exit.");
                Console.Write("Choose product: ");

                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        productManager.AddProduct();
                        break;
                    case "2":
                        productManager.EditProduct();
                        break;
                    case "3":
                        productManager.DeleteProduct();
                        break;
                    case "4":
                        productManager.ListProducts();
                        break;
                    case "5":
                        productManager.SearchProducts();
                        break;
                    case "0":
                        Console.WriteLine("See you later!");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}