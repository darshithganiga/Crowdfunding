using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;

namespace DataStore.Abstraction.IRepositry
{
    public interface ICampaignDetailsRepo
    {
        Task<CampaignDetailDTO> GetCampaign(int CampaignId);

    }
}
