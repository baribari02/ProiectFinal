using System;

class Program
{
    static void Main()
    {
        var manager = new Manager();
        while (true)
        {
            Console.WriteLine("\n=== Main Menu ===");
            Console.WriteLine("1. Register   2. Login   3. List Users   4. List Products   5. Search Products");
            Console.WriteLine("6. Filter Products   7. Add Product   8. Edit Product   9. Delete Product   10. Order Summary");
            Console.WriteLine("11. Add To Cart   12. Remove From Cart   13. View Cart   14. Checkout   0. Exit");
            Console.Write("Choice: ");
            var input = Console.ReadLine();
            try
            {
                switch (input)
                {
                    case "1": manager.Register(); break;
                    case "2": manager.Login(); break;
                    case "3": manager.ListUsers(); break;
                    case "4": manager.ListProducts(); break;
                    case "5": manager.SearchProducts(); break;
                    case "6": manager.FilterProductsByPrice(); break;
                    case "7": if (manager.IsAdmin()) manager.AddProduct(); else manager.Error(); break;
                    case "8": if (manager.IsAdmin()) manager.EditProduct(); else manager.Error(); break;
                    case "9": if (manager.IsAdmin()) manager.DeleteProduct(); else manager.Error(); break;
                    case "10": manager.OrderSummary(); break;
                    case "11": if (manager.IsLogged()) manager.AddToCart(); else manager.Error(); break;
                    case "12": if (manager.IsLogged()) manager.RemoveFromCart(); else manager.Error(); break;
                    case "13": if (manager.IsLogged()) manager.ViewCart(); else manager.Error(); break;
                    case "14": if (manager.IsLogged()) manager.Checkout(); else manager.Error(); break;
                    case "0": manager.SaveAll(); return;
                    default: Console.WriteLine("Invalid."); break;
                }
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
        }
    }
}