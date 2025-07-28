using System;

public class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } // e.g., "customer" or "admin"
    public string Password { get; set; }

    public User(int id, string name, string email, string role, string password)
    {
        ID = id; Name = name; Email = email; Role = role; Password = password;
    }
    public override string ToString() => $"[{ID}] {Name} ({Email}) - {Role}";
}