using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;

namespace FeatureObjects.Abstraction.Managers
{
    public interface IInvestmentCalcManager
    {
        Task<InvestmentResultDTO> CalculateInvestment(InvestmentCalculationDTO investmentCalculationDTO);
        Task<bool> HandleSuccessfulPayment(
            string transactionId, int userId, int campaignId, int sharesBought,
            decimal amountInvested, decimal equityOwned, DateTime investmentDate);
        Task<InvestmentPaymentDTO> GetInvestmentPayments(int userId);
    }
}
