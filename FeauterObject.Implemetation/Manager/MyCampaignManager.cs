using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Repositories;
using FeatureObjects.Abstraction.Managers;

namespace FeatureObjects.Implementation.Managers
{
    public class MyCampaignManager : IMyCampaignManager
    {
        private readonly IMyCampaignRepository _repository;
        public MyCampaignManager(IMyCampaignRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<HomePageDTO>> GetAllFundraiserCampaigns(int FundraiserId)
        {
            return await _repository.GetFundraiserCampaigns(FundraiserId);
        }
        public async Task<CampaignDashboardDTO> GetFundraiserCampaignDetails(int CampaignId)
        {
            return await _repository.GetFundraiserCampaignDetails(CampaignId);
        }
    }
}
