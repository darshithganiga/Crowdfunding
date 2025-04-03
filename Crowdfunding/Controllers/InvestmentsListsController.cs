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
    public class InvestmentsListsController:ControllerBase
    {
        private readonly IInvestmentsListsManager _investmentslists;
        public InvestmentsListsController(IInvestmentsListsManager investmentslists)
        {
            _investmentslists = investmentslists;
        }
        [HttpGet("Investments")]
        
        public async Task<ActionResult<IEnumerable<InvestmentListsModel>>> GetInvestmentLists()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var UserId = int.Parse(userIdClaim);

            var investmentlist = await _investmentslists.InvestmentsListsAsync(UserId);
            return Ok(investmentlist);
        }
    }
}
