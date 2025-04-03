using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.IRepositry;
using DataStore.Abstraction.Models;
using FeauterObject.abstraction.Manager;

namespace FeauterObject.Implemetation.Manager
{
    public class DeleteCampaignManager:IDeleteCampaignManager
    {
        private readonly IDeleteCampaignRepo _repo;

        public DeleteCampaignManager(IDeleteCampaignRepo repo) {

            _repo = repo;

        }

        public async Task<bool> DeleteCampaign(int campaignId, int userId)
        {
            return await _repo.DeleteCampaign(campaignId, userId);
        }
    }
}
