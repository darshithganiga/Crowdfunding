using System.Security.Claims;
using DataStore.Abstraction.DTO;
using FeatureObject.Abstraction.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundingWebsite.Controllers
{
    [Authorize]
    [ApiController]
    [Route("investor")]
    public class InvestmentDetailsController:ControllerBase
    {
        private readonly IInvestmentDetailsManager _manager;
        public InvestmentDetailsController(IInvestmentDetailsManager manager)
        {
            _manager = manager;
        }
        [HttpGet("{campaignId}/investmentdetails")]
       
        public async Task<ActionResult<InvestmentDetails>> GetUniqueInvestmentDetails(int campaignId)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int userid = int.Parse(id);

            if (string.IsNullOrEmpty(id))
            {
                return Unauthorized("FundraiserId not found in token.");
            }

            var investmentdetails = await _manager.GetUniqueInvestmentDetailsAsync(campaignId, int.Parse(id));
            return Ok(investmentdetails);
        }
    }
}