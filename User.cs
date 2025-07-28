using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj1._0
{
    internal class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(int id, string name, string email, string password)
        {
            ID = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        public override string ToString()
        {
            return $"{ID}. {Name} ({Email})";
        }
    }
}