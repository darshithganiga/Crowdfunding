using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Models;

namespace FeauterObject.abstraction.Manager
{
    public interface IHomePageManager
    {

        Task<IEnumerable<HomePageDTO>> GetAllDetails();
    }
}
