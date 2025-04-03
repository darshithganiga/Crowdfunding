using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using DataStore.Abstraction.IRepositry;
using DataStore.Abstraction.Models;

namespace Datastore.implementation.Repository
{
    public class PostCampaignRepo:IPostCampaign
    {

        private readonly DapperContext _dapperContext;

        public PostCampaignRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<(bool IsSuccess, string Message)> PostCampaigndetails(CampaignModel model)
        {
            using (var connection = _dapperContext.createConnection())
            {
              
                string checkSql = "SELECT COUNT(1) FROM Campaigns WHERE CompanyName = @CompanyName";
                int existingCount = await connection.ExecuteScalarAsync<int>(checkSql, new { model.CompanyName });

                if (existingCount > 0)
                {
                    return (false, "A campaign with the same company name already exists.");
                }

               
                string insertSql = @"
            INSERT INTO Campaigns 
            (FundraiserID, CompanyName, ProductImage, Description, FundingGoal, EquityOffered, SharesTotal, SharesRemaining, StartDate, EndDate) 
            VALUES 
            (@FundraiserID, @CompanyName, @ProductImage, @Description, @FundingGoal, @EquityOffered, @SharesTotal, @SharesRemaining, @StartDate, @EndDate)";

                int rowsAffected = await connection.ExecuteAsync(insertSql, new
                {
                    model.FundraiserID,
                    model.CompanyName,
                    model.ProductImage,
                    model.Description,
                    model.FundingGoal,
                    model.EquityOffered,
                    model.SharesTotal,
                    SharesRemaining = model.SharesTotal,
                    model.StartDate,
                    model.EndDate
                });

                return rowsAffected > 0
                    ? (true, "Campaign added successfully.")
                    : (false, "Failed to add campaign.");
            }
        }



    }


}
