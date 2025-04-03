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
    public class PostCampaignManager:IPostCampaignManager
    {
        private readonly IPostCampaign _repo;

        public PostCampaignManager(IPostCampaign repo)
        {
            _repo = repo;
        }

      public async  Task<(bool IsSuccess, string Message)> PostCampaignDetails(CampaignModel model)
        {
            return await _repo.PostCampaigndetails(model);
        }


    }
}
