using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.IRepositry;
using DataStore.Abstraction.Models;
using FeauterObject.abstraction.Manager;

namespace FeauterObject.Implemetation.Manager
{
    public class HomePageManager:IHomePageManager
    {
        private readonly IHomePageRepo _repo;

        public HomePageManager(IHomePageRepo repo)
        {
            _repo = repo;
        }

        public async Task <IEnumerable<HomePageDTO>> GetAllDetails()
        {
            return await _repo.GetAllCampaigns();
        }


    }
}
