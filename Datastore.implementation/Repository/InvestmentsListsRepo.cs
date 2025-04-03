using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Datastore.implementation;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.IRepository;
using DataStore.Abstraction.Models;

namespace DataStore.Implementation.Repository
{
    public class InvestmentsListsRepo:IInvestmentsListsRepo
    {
        private readonly DapperContext _dapperContext;
        public InvestmentsListsRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<InvestmentListsModel>> GetInvestmentsAsync(int UserId)
        {
            using(var connection = _dapperContext.createConnection())
            {

                // fetches info about all the campaigns that user has invested in
                var query = @"
                SELECT 
                i.CampaignID,
                MAX(i.InvestmentId) AS InvestmentId, 
                SUM(i.AmountInvested) AS AmountInvested, 
                c.CompanyName,
                c.ProductImage,
                SUM(i.ShareBuyed) AS ShareBuyed,
                SUM(i.EquityOwned) AS EquityOwned 
                FROM Investments i
                INNER JOIN Campaigns c ON i.CampaignID = c.CampaignID
                WHERE i.UserId = @UserId
                GROUP BY i.CampaignID, c.CompanyName, c.ProductImage;";

                return await connection.QueryAsync<InvestmentListsModel>(query, new { UserId = UserId });
            }
        }
    }
}