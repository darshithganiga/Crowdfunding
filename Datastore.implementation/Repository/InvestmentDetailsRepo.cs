using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Datastore.implementation;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.IRepository;
using DataStore.Abstraction.Models;

namespace DataStore.Implementation.Repository
{
    public class InvestmentDetailsRepo:IInvestmentDetailsRepo
    {
        private readonly  DapperContext _dapperContext;
        public InvestmentDetailsRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<InvestmentDetails> GetUniqueInvestmentDetailsAsync(int campaignId,int id)
        {
            using (var connection = _dapperContext.createConnection())
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Query to fetch single campaign by CampaignId
                        var query = @"
                            SELECT
                            i.CampaignId,
                            c.CompanyName,
                            c.ProductImage,
                            c.FundingGoal,
                            c.FundsRaised,
                            c.EquityOffered,
                            c.SharesTotal,
                            c.SharesRemaining,
                            c.StartDate,
                            c.EndDate,
                            c.Status,
                            SUM(i.AmountInvested) AS TotalAmountInvested, 
                            SUM(i.ShareBuyed) AS TotalSharesBought, 
                            SUM(i.EquityOwned) AS TotalEquityOwned, 
                            MAX(i.InvestmentDate) AS LastInvestmentDate 
                            FROM Investments i
                            JOIN Campaigns c ON i.CampaignId = c.CampaignId
                            WHERE i.UserId = UserId AND c.CampaignID = @CampaignId
                            GROUP BY i.CampaignId, c.CompanyName, c.ProductImage, c.FundingGoal, c.FundsRaised, c.EquityOffered, 
                c.SharesTotal, c.SharesRemaining, c.StartDate, c.EndDate, c.Status;";

                        
                        var campaign = await connection.QuerySingleOrDefaultAsync<InvestmentDetails>(
                            query, new { CampaignId = campaignId }, transaction);

                        if (campaign == null)
                        {
                            return null;
                        }

                        // Query to fetch all investors for this campaign
                        string investorQuery = @"
                                                SELECT 
                                                i.InvestmentDate,
                                                p.TransactionId,
                                                p.PaymentMethod,
                                                i.AmountInvested,
                                                i.ShareBuyed,
                                                i.EquityOwned
                                                FROM Investments i
                                                LEFT JOIN Payment p ON i.InvestmentId = p.InvestmentId
                                                WHERE i.UserId = @UserId AND i.CampaignId = @CampaignId
                                                ORDER BY i.InvestmentDate DESC;";

                        var seperateinvestment = (await connection.QueryAsync<SeperateInvestmentsDTO>(investorQuery, new { UserId = id, CampaignId = campaignId }, transaction)).ToList();
                        Console.WriteLine(seperateinvestment);

                        // Assign investors to campaign
                        campaign.SeparateInvestmnet = seperateinvestment;

                        transaction.Commit();
                        return campaign; //  Returns only one campaign
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Error in CampaigDashboardAsync: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}
