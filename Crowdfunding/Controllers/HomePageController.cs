using Crowdfunding.DTO;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Models;
using FeauterObject.abstraction.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfunding.Controllers
{
    [ApiController]
    [Route("/HomePage")]
    public class HomePageController:ControllerBase
    {

        private readonly IHomePageManager _manager;

        public HomePageController(IHomePageManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<HomePageDTO>>> GetAllCampaignDetails()
        {
            var details = await _manager.GetAllDetails();
            return Ok(details);
        }


    }
}
