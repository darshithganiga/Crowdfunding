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
    public class InvestmentDetailsManager:IInvestmentDetailsManager
    {
        private readonly IInvestmentDetailsRepo _repository;
        public InvestmentDetailsManager(IInvestmentDetailsRepo repository)
        {
            _repository = repository;
        }
        public async Task<InvestmentDetails> GetUniqueInvestmentDetailsAsync(int campaignId,int id)
        {
            return await _repository.GetUniqueInvestmentDetailsAsync(campaignId,id);

        }
    }
}
