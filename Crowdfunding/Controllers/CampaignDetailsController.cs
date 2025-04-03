using DataStore.Abstraction.DTO;
using FeauterObject.abstraction.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfunding.Controllers
{

    [ApiController]
    [Route("/campaigns")]
    public class CampaignDetailsController:ControllerBase
    {

        private readonly ICampaignDetailsManager _campaignDetailsManager;
        public CampaignDetailsController(ICampaignDetailsManager campaignDetailsManager)
        {
            _campaignDetailsManager = campaignDetailsManager;
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<CampaignDetailDTO>> CampaignDetails(int id)
        {
            var details = await _campaignDetailsManager.GetParticularCampaign(id);
            return Ok(details);
        }
    }
}
