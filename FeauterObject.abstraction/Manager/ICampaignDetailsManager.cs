using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;

namespace FeauterObject.abstraction.Manager
{
    public interface ICampaignDetailsManager
    {

        Task<CampaignDetailDTO> GetParticularCampaign(int campaignid);    }
}
