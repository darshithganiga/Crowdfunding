using System.Security.Claims;
using Crowdfunding.DTO;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Models;
using FeauterObject.abstraction.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfunding.Controllers
{
    [ApiController]
    [Route("/delete")]
    public class DeleteCampaignController:ControllerBase
    {
        private readonly IDeleteCampaignManager _manager;

        public DeleteCampaignController(IDeleteCampaignManager manager)
        {
            _manager = manager;
        }

        [HttpDelete("{CampaignID}")]
        [Authorize] // Only fundraiser can delete
        public async Task<IActionResult> DeleteCampaign(int CampaignID)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token.");

            var userId = int.Parse(userIdClaim);

            var isDeleted = await _manager.DeleteCampaign(CampaignID, userId);
            if (!isDeleted) return NotFound("Campaign not found or unauthorized action.");

            return Ok("Campaign deleted successfully.");
        }


    }
}
