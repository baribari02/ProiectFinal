using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace proiectFinal
{
    internal class UserManager
    {
        public List<User> Users { get; private set; } = new();
        private string fileName = "users.csv";

        public void LoadUsers()
        {
            if (!File.Exists(fileName)) return;
            Users = File.ReadAllLines(fileName)
                .Select(line =>
                {
                    var s = line.Split(',');
                    return new User
                    {
                        ID = int.Parse(s[0]),
                        Name = s[1],
                        Email = s[2],
                        Password = s[3]
                    };
                }).ToList();
        }

        public void SaveUsers()
        {
            File.WriteAllLines(fileName,
                Users.Select(u => $"{u.ID},{u.Name},{u.Email},{u.Password}"));
        }

        public User SignUp(string name, string email, string password)
        {
            if (Users.Any(u => u.Email == email))
                throw new Exception("Email already exists.");

            int newId = Users.Any() ? Users.Max(u => u.ID) + 1 : 1;
            var user = new User(newId, name, email, password);
            Users.Add(user);
            SaveUsers();
            return user;
        }

        public User Login(string email, string password)
        {
            var user = Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;
            return user.CheckPassword(password) ? user : null;
        }

        public void ListUsers()
        {
            foreach (var user in Users)
                Console.WriteLine(user);
        }
        public static bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
