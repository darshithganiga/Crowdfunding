
using System.Diagnostics;

using Dapper;
using Datastore.implementation;
using DataStore.Abstraction.DTO;
using DataStore.Abstraction.Exceptions;
using DataStore.Abstraction.IRepositry;
using Microsoft.Extensions.Logging; // For logging


namespace Datastore.Implementation.Repository
{
    public class HomePageRepo : IHomePageRepo
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger<HomePageRepo> _logger; // Inject Logger for better insights

        public HomePageRepo(DapperContext dapperContext, ILogger<HomePageRepo> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }


       
        public async Task<IEnumerable<HomePageDTO>> GetAllCampaigns()
        {
            string query = @"
    SELECT 
        CampaignID,
        CompanyName,
        ProductImage,
        FundingGoal,
        FundsRaised,
        EndDate,
        Status 
    FROM Campaigns
    WHERE Status = 'Active'";

            try
            {
                using (var connection = _dapperContext.createConnection())

                {
                    var stopwatch = Stopwatch.StartNew();
                    var campaigns = await connection.QueryAsync<HomePageDTO>(query);
                    stopwatch.Stop(); 
                    Console.WriteLine($"Query Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                    return campaigns;
                }
            }
            catch (DatabaseException ex)
            {
                _logger.LogError($"Database error occurred: {ex.Message}");

                
                throw new DatabaseException("An error occurred while fetching campaigns. Please try again later.",ex); 
            }
        }
    }
}
