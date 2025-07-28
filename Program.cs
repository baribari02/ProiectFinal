namespace proiectFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product(1, "Laptop", "High performance laptop", 1500.00, 10);
            Console.WriteLine(product1);

            var userService = new UserManager();
            userService.LoadUsers();

            while (true)
            {
                Console.WriteLine("\n1. SignUp");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. List Users");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                var opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        Console.Write("Name: ");
                        var name = Console.ReadLine();
                        string email;
                        do
                        {
                            Console.Write("Email: ");
                            email = Console.ReadLine();
                            if (!UserManager.IsValidEmail(email))
                                Console.WriteLine("Email invalid! Încearcă din nou.");
                        } while (!UserManager.IsValidEmail(email));

                        string pass, confirm;
                        do
                        {
                            Console.Write("Password: ");
                            pass = Console.ReadLine();
                            Console.Write("Confirm: ");
                            confirm = Console.ReadLine();
                            if (pass != confirm)
                                Console.WriteLine("Passwords do not match!");
                        } while (pass != confirm);
                        try
                        {
                            userService.SignUp(name, email, pass);
                            Console.WriteLine("Registered successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case "2":
                        while (true)
                        {
                            Console.Write("Email (sau Enter for back): ");
                            email = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(email))
                                break;
                            Console.Write("Password: ");
                            pass = Console.ReadLine();
                            var user = userService.Login(email, pass);
                            if (user != null)
                            {
                                Console.WriteLine($"Welcome, {user.Name}!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Email or password is incorrect! Try Again.");
                            }
                        }
                        break;

                    case "3":
                        userService.ListUsers();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;

                }
            }
        }
    }
}
