using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.Models;

namespace FeauterObject.abstraction.Manager
{
    public interface IPostCampaignManager
    {

        Task<(bool IsSuccess, string Message)> PostCampaignDetails(CampaignModel model);
    }
}
