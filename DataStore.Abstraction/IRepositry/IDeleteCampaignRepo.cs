using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.IRepositry
{
    public interface IDeleteCampaignRepo
    {
        Task<bool> DeleteCampaign(int campaignId, int userId);


    }
}
