using System;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Repositories;
using FeatureObjects.Abstraction.Managers;

namespace FeatureObjects.Implementation.Managers
{
    public class InvestmentCalcManager : IInvestmentCalcManager
    {
        private readonly IInvestmentRepository _repository;

        public InvestmentCalcManager(IInvestmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<InvestmentResultDTO> CalculateInvestment(InvestmentCalculationDTO investmentCalculationDTO)
        {

            decimal investorEquity = (investmentCalculationDTO.SharesToBuy / (decimal)investmentCalculationDTO.TotalShares) * investmentCalculationDTO.EquityOffered;
            decimal investmentAmount = (investmentCalculationDTO.FundingGoal / investmentCalculationDTO.TotalShares) * investmentCalculationDTO.SharesToBuy;
            return new InvestmentResultDTO
            {
                InvestorEquity = investorEquity,
                InvestmentAmount = investmentAmount
            };
        }

        public async Task<bool> HandleSuccessfulPayment(
            string transactionId, int userId, int campaignId, int sharesBought,
            decimal amountInvested, decimal equityOwned, DateTime investmentDate)
        {
            return await _repository.CreateInvestment(
                transactionId, userId, campaignId, sharesBought, amountInvested, equityOwned, investmentDate);
        }
        public async Task<InvestmentPaymentDTO> GetInvestmentPayments(int userId)
        {
            return await _repository.GetInvestmentPayments(userId);
        }
    }
}
