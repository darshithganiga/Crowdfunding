using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Models;

namespace DataStore.Abstraction.IRepositry
{
    public interface IHomePageRepo
    {
        Task<IEnumerable<HomePageDTO>> GetAllCampaigns();

    }
}
