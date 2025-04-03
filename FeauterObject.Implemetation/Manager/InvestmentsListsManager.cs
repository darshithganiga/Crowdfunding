using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.IRepository;
using FeatureObject.Abstraction.Manager;

namespace FeatureObjects.Implementation.Manager
{
    public class InvestmentsListsManager:IInvestmentsListsManager
    {
        private readonly IInvestmentsListsRepo _investmentslists;
        public InvestmentsListsManager(IInvestmentsListsRepo investmentslists)
        {
            _investmentslists = investmentslists;
        }
        public async Task<IEnumerable<InvestmentListsModel>> InvestmentsListsAsync(int UserId)
        {
            return await _investmentslists.GetInvestmentsAsync(UserId);
        }
    }
}
