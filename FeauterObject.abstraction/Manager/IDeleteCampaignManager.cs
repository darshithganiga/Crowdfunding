using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeauterObject.abstraction.Manager
{
    public interface IDeleteCampaignManager
    {
        Task<bool> DeleteCampaign(int campaignId, int userId);

    }
}
