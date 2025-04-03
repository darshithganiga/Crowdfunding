using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Datastore.implementation;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Repositories;

namespace DataStore.Implementation.Repositories
{
    public class MyCampaignRepository : IMyCampaignRepository
    {
        private readonly DapperContext _dapperContext;
        public MyCampaignRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<HomePageDTO>> GetFundraiserCampaigns(int fundraierId)
        {
            using (var connection = _dapperContext.createConnection())
            {
                string query = "SELECT CampaignID,CompanyName,ProductImage,FundingGoal,FundsRaised,EndDate FROM  Campaigns WHERE FundraiserID=@FundraiserID";
                return await connection.QueryAsync<HomePageDTO>(query, new { FundraiserID = fundraierId });
            }
        }

        public async Task<CampaignDashboardDTO> GetFundraiserCampaignDetails(int campaignId)
        {
            using (var connection = _dapperContext.createConnection())
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Query to fetch single campaign by CampaignId
                        string campaignQuery = @"
                        SELECT 
                            c.CampaignId,
                            c.CompanyName,
                            c.ProductImage,
                            c.Description,
                            c.FundingGoal,
                            c.FundsRaised,
                            c.EquityOffered,
                            c.SharesTotal,
                            c.SharesRemaining,
                            c.StartDate,
                            c.EndDate,
                            c.Status
                        FROM Campaigns c
                        WHERE c.CampaignID = @CampaignId;";

                        // Get single campaign
                        var campaign = await connection.QuerySingleOrDefaultAsync<CampaignDashboardDTO>(
                            campaignQuery, new { CampaignId = campaignId }, transaction);

                        // If no campaign is found, return null
                        if (campaign == null)
                        {
                            return null;
                        }

                        // Query to fetch all investors for this campaign
                        string investorQuery = @"
                        SELECT 
                            i.CampaignId,
                            u.Name AS InvestorName,
                            i.AmountInvested,
                            i.InvestmentDate,
                            i.ShareBuyed,
                            i.EquityOwned
                        FROM Investments i
                        JOIN Users u ON i.UserId = u.UserID
                        WHERE i.CampaignId = @CampaignId;";

                        var investors = (await connection.QueryAsync<CampaignInvesters>(
                            investorQuery, new { CampaignId = campaignId }, transaction)).ToList();

                        // Assign investors to campaign
                        campaign.CampaignInvesters = investors;

                        transaction.Commit();
                        return campaign; // ✅ Returns only one campaign
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
