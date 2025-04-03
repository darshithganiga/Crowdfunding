using DataStore.Abstraction.Models;
using System.Security.Claims;
using FeauterObject.abstraction.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Crowdfunding.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/campaign")]
    public class PostCampaignController:ControllerBase
    {
        private readonly IPostCampaignManager _manager;

        public PostCampaignController(IPostCampaignManager manager)
        {
            _manager = manager;
        }

        [HttpPost("create-campaign")]
       

        public async Task<IActionResult> CreateCampaign([FromBody] CampaignModel model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
               
                return Unauthorized("User ID not found in token");

            }

            Console.WriteLine(int.Parse(userIdClaim));
            
            model.FundraiserID = int.Parse(userIdClaim);

            var (success,message)=await _manager.PostCampaignDetails(model);

            if (!success)
            {
                return BadRequest(new { Message = message});
            }

            return Ok(new { Message = message});
        }



    }
}
