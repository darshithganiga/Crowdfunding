using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Datastore.implementation;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Repositories;
//using Microsoft.AspNetCore.Http.HttpResults;

namespace DataStore.Implementation.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly DapperContext _dapperContext;

        public InvestmentRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<bool> CreateInvestment(
            string transactionId, int userId, int campaignId, int sharesBought,
            decimal amountInvested, decimal equityOwned, DateTime investmentDate)
        {
            using (var connection = _dapperContext.createConnection())
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert investment details
                        string investmentQuery = @"
                            INSERT INTO Investments (UserId, CampaignId, AmountInvested, InvestmentDate, ShareBuyed, EquityOwned, CreatedAt)
                            VALUES (@UserId, @CampaignId, @AmountInvested,@InvestmentDate, @SharesBought, @EquityOwned, @CreatedAt );
                            SELECT CAST(SCOPE_IDENTITY() AS INT);
                        ";

                        int investmentId = await connection.ExecuteScalarAsync<int>(investmentQuery,
                            new
                            {
                                UserId = userId,
                                CampaignId = campaignId,
                                AmountInvested = amountInvested,
                                InvestmentDate = investmentDate,
                                SharesBought = sharesBought,
                                EquityOwned = equityOwned,
                                CreatedAt = investmentDate
                            }, transaction);

                // Insert payment details
                string paymentQuery = @"
                            INSERT INTO Payment (InvestmentId,UserId, TransactionId, PaymentMethod, PaymentStatus)
                            VALUES (@InvestmentId,@UserId, @TransactionId, 'Stripe', 'Success');
                        ";

                await connection.ExecuteAsync(paymentQuery,
                    new {InvestmentId = investmentId, UserId = userId, TransactionId = transactionId }, transaction);

                // Update campaign details
                string updateCampaignQuery = @"
                            UPDATE Campaigns 
                            SET FundsRaised = FundsRaised + @AmountInvested,
                                SharesRemaining = SharesRemaining - @SharesBought
                            WHERE CampaignID = @CampaignId;
                        ";

                await connection.ExecuteAsync(updateCampaignQuery,
                    new { AmountInvested = amountInvested, SharesBought = sharesBought, CampaignId = campaignId },transaction);

                        transaction.Commit();


                        await connection.ExecuteAsync("UpdateCampaignStatus5", new { CampaignId = campaignId }, commandType: CommandType.StoredProcedure);

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public async Task<InvestmentPaymentDTO> GetInvestmentPayments(int userId)
        {
            using (var connection = _dapperContext.createConnection())
            {
                //query to display in payment success page
                string query = @"
SELECT 
    p.TransactionId,
    p.PaymentMethod,
    p.PaymentStatus,
    i.InvestmentId,
    i.UserId,
    i.CampaignId,
    i.AmountInvested,
    i.InvestmentDate,
    i.ShareBuyed,
    i.EquityOwned
FROM Payment p
INNER JOIN Investments i ON p.InvestmentId = i.InvestmentId
WHERE i.UserId = @UserId ORDER BY i.investmentDate DESC";

                return await connection.QueryFirstOrDefaultAsync<InvestmentPaymentDTO>(query, new { UserId = userId });
            }
        }

    }
}
