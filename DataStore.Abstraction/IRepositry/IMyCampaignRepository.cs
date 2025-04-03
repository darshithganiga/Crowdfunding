using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;

namespace DataStore.Abstraction.Repositories
{
    public interface IMyCampaignRepository
    {

        Task<IEnumerable<HomePageDTO>> GetFundraiserCampaigns(int fundraierId);
        Task<CampaignDashboardDTO> GetFundraiserCampaignDetails(int campaignId);
    }
}
