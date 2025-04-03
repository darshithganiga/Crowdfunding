using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataStore.Abstraction.Exceptions;
using DataStore.Abstraction.IRepositry;
using DataStore.Abstraction.Models;

namespace Datastore.implementation.Repository
{
    public class DeleteCampaignRepo : IDeleteCampaignRepo
    {
        private readonly DapperContext _dapperContext;

        public DeleteCampaignRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<bool> DeleteCampaign(int campaignId, int userId)
        {
            const string sql = @"DELETE FROM Campaigns WHERE CampaignID = @CampaignId AND FundraiserID = @UserId ";

            using var connection = _dapperContext.createConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, new { CampaignId = campaignId, UserId = userId });

        

           
                return rowsAffected > 0;
            }
        }
    }



