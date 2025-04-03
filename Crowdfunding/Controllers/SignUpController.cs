using System.Data;
using Datastore.implementation.Models;

using DataStore.Abstraction.Models;
using FeauterObject.abstraction.Manager;

using Microsoft.AspNetCore.Mvc;

namespace Crowdfunding.Controllers
{
    [ApiController]
    [Route("/SignUp")]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpManager _manager;

        public SignUpController(ISignUpManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> Signingin(UserModel model)
        {
            var token = await _manager.SignInUser(model);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(token);
        }
    }
}
