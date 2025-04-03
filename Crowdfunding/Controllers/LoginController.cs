

using Crowdfunding.DTO;
using DataStore.Abstraction.Models;
using FeauterObject.abstraction.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfunding.Controllers
{
    [ApiController]
    [Route("/Login")]
    public class LoginController : ControllerBase
    {

        public readonly ILoginManager _manager;

        public LoginController(ILoginManager manager)
        {
            _manager = manager;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto logindto)
        {
            var token = await _manager.ValidateUser(logindto.PasswordHash, logindto.Email);
            if (token == null)
            {
                return Unauthorized("Invalid username or Password");
            }
            return Ok(token);





        }
    }
}
