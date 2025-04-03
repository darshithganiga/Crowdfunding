namespace Crowdfunding.Services
{
    public interface IPasswordHasher
    {
        string HashPasswords(string password);
        
    }
}
