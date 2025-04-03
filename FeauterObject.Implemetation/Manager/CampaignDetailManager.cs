using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.IRepositry;
using FeauterObject.abstraction.Manager;

namespace FeauterObject.Implemetation.Manager
{
    public class CampaignDetailManager:ICampaignDetailsManager
    {
        private readonly ICampaignDetailsRepo _repo;

        public CampaignDetailManager( ICampaignDetailsRepo repo)
        {
            _repo = repo;
        }

        public async Task<CampaignDetailDTO> GetParticularCampaign(int campaignid)
        {
            return await _repo.GetCampaign(campaignid);
        }

    }
}
