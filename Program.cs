using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace proiectFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                            pass = ReadPassword();
                            Console.Write("Confirm: ");
                            confirm = ReadPassword();
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
                            Console.Write("Email (sau Enter pentru a reveni): ");
                            email = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(email))
                                break;
                            Console.Write("Password: ");
                            pass = ReadPassword();
                            var user = userService.Login(email, pass);
                            if (user != null)
                            {
                                Console.WriteLine($"Welcome, {user.Name}!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Email or password is incorrect! Încearcă din nou.");
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
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        int cursorLeft = Console.CursorLeft;
                        if (cursorLeft > 0)
                        {
                            Console.SetCursorPosition(cursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(cursorLeft - 1, Console.CursorTop);
                        }
                    }
                }
                else if (keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}