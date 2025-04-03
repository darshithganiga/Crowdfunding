using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;

namespace FeatureObject.Abstraction.Manager
{
    public interface IInvestmentsListsManager
    {
        Task<IEnumerable<InvestmentListsModel>> InvestmentsListsAsync(int UserId);
    }
}
