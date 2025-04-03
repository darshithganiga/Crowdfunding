namespace Crowdfunding.Services
{
    public class PasswordHasher:IPasswordHasher
    {
        public string HashPasswords(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


    }
}
