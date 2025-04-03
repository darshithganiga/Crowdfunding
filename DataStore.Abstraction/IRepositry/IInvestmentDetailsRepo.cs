using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;

namespace DataStore.Abstraction.IRepository
{
    public interface IInvestmentDetailsRepo
    {
        Task<InvestmentDetails> GetUniqueInvestmentDetailsAsync(int campaignId,int id);
    }
}
