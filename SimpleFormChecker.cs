public class SimpleFormChecker : IFormChecking
{
    public bool ValidateEmail(string email) => email.Contains("@") && email.Contains(".");
    public bool ValidatePassword(string password) => password.Length >= 6;
}