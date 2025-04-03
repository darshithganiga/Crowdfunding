using System.Security;

namespace Crowdfunding.DTO
{
    public class LoginDto
    {

        
        public string Email { get; set; }
        public string PasswordHash { get; set; }    
    }
}
