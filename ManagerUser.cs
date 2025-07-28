using FinalProj1._0;
using System;
using System.Collections.Generic;

namespace proiectFinal
{
    
    internal class ManagerUser
    {
        private User _user;
        private List<string> _cart;
        private List<string> _wishlist;

        public ManagerUser(User user)
        {
            _user = user;
            _cart = new List<string>();
            _wishlist = new List<string>();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine($"\nWelcome, {_user.Name}! Please choose an option:");
                Console.WriteLine("1. Add item(s) to cart");
                Console.WriteLine("2. Add item to wishlist");
                Console.WriteLine("3. View cart");
                Console.WriteLine("4. View wishlist ♡: ");
                Console.WriteLine("5. Remove item from cart");
                Console.WriteLine("6. Checkout");
                Console.WriteLine("0. Exit");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddItemsToCart();
                        break;
                    case "2":
                        AddItemToWishlist();
                        break;
                    case "3":
                        ViewCart();
                        break;
                    case "4":
                        ViewWishlist();
                        break;
                    case "5":
                        RemoveItemFromCart();
                        break;
                    case "6":
                        Checkout();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void AddItemsToCart()
        {
            Console.Write("How many items would you like to add? ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.Write($"Enter name of item #{i + 1}: ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    _cart.Add(item);
                    Console.WriteLine($"Added \"{item}\" to cart.");
                }
                else
                {
                    Console.WriteLine("Item name cannot be empty. Skipping.");
                }
            }
        }

        private void AddItemToWishlist()
        {
            Console.Write("Enter the name of the item to add to wishlist: ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                _wishlist.Add(item);
                Console.WriteLine($"Added \"{item}\" to wishlist.");
            }
            else
            {
                Console.WriteLine("Item name cannot be empty.");
            }
        }

        private void ViewCart()
        {
            Console.WriteLine("Your cart:");
            if (_cart.Count == 0)
            {
                Console.WriteLine("  (Cart is empty)");
            }
            else
            {
                for (int i = 0; i < _cart.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {_cart[i]}");
                }
            }
        }

        private void ViewWishlist()
        {
            Console.WriteLine("Your wishlist:");
            if (_wishlist.Count == 0)
            {
                Console.WriteLine("  (Wishlist is empty)");
            }
            else
            {
                for (int i = 0; i < _wishlist.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {_wishlist[i]}");
                }
            }
        }

        private void RemoveItemFromCart()
        {
            if (_cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            ViewCart();
            Console.Write("Enter the number of the item to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index <= 0 || index > _cart.Count)
            {
                Console.WriteLine("Invalid item number.");
                return;
            }
            string removedItem = _cart[index - 1];
            _cart.RemoveAt(index - 1);
            Console.WriteLine($"Removed \"{removedItem}\" from cart.");
        }

        private void Checkout()
        {
            if (_cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty. Add items before checking out.");
                return;
            }

            Console.WriteLine("Checking out the following items:");
            foreach (var item in _cart)
            {
                Console.WriteLine($"- {item}");
            }
            _cart.Clear();
            Console.WriteLine("Order placed! Your cart is now empty.");
        }
    }
}