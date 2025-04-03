using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataStore.Abstraction.DTO;
using FeatureObjects.Abstraction.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Authorize]
    [ApiController]
    [Route("fundraiser")]
   
    public class MyCampaignController : ControllerBase
    {
        private readonly IMyCampaignManager _manager;
        public MyCampaignController(IMyCampaignManager manager)
        {
            _manager = manager;
        }
        [HttpGet("campaigns")]
        

        public async Task<ActionResult<IEnumerable<HomePageDTO>>> GetAllCampaigns()
        {
            var FundraiserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;



            if (string.IsNullOrEmpty(FundraiserId))
            {
                return Unauthorized("FundraiserId not found in token.");
            }
            var campaigns = await _manager.GetAllFundraiserCampaigns(int.Parse(FundraiserId));
            return Ok(campaigns);
        }
        [HttpGet("campaigns/campaigndetails")]
        public async Task<ActionResult<CampaignDashboardDTO>> GetFundraiserCampaignDetails(int CampaignId)
        {
            var FundraiserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (string.IsNullOrEmpty(FundraiserId))
            {
                return Unauthorized("FundraiserId not found in token.");
            }
            var investmentdetails = await _manager.GetFundraiserCampaignDetails(CampaignId);
            return Ok(investmentdetails);
        }
    }
}
