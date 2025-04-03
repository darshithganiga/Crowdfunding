using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Datastore.Implementation.Repository;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Exceptions;
using DataStore.Abstraction.IRepositry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Datastore.implementation.Repository
{
    public class CampaignDetailsRepo : ICampaignDetailsRepo
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger<CampaignDetailsRepo> _logger;

        public CampaignDetailsRepo(DapperContext dapperContext,ILogger<CampaignDetailsRepo> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<CampaignDetailDTO> GetCampaign(int CampaignId)
        {

            try {
                using (var conn = _dapperContext.createConnection())
                {
                    var sql = @"SELECT e.CampaignID ,e.ProductImage, e.CompanyName, e.Description, e.FundingGoal,e.FundsRaised,e.SharesTotal, e.EquityOffered,e.SharesRemaining, e.EndDate FROM Campaigns e WHERE e.CampaignID=@CampaignID ;";
                var det=     await conn.QueryFirstOrDefaultAsync<CampaignDetailDTO>(sql, new { CampaignID = CampaignId });

                    if (det == null)
                    {
                        throw new NullException($"The {CampaignId} rteurned null value from the database");

                    }
                    {
                        
                    }

                    return det;

                }

               
        }
            catch(NullException ex)
            {
                _logger.LogError($"The Null Value has been returned.{ex.Message}");

                throw new NullException(" The database has returned a null value");
            }




    }
}}
