using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.Models;

namespace DataStore.Abstraction.IRepositry
{
    public interface IPostCampaign
    {
        Task<(bool IsSuccess, string Message)> PostCampaigndetails(CampaignModel model);

    }
}
