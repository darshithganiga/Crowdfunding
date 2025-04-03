using System;
using System.Threading.Tasks;
using Dapper;
using Datastore.implementation;
using Datastore.implementation.Models;
using DataStore.Abstraction.AbstractionModel;
using DataStore.Abstraction.IRepositry;
using DataStore.Abstraction.Models;

namespace Datastore.Implementation.Repository
{
    public class SignUpRepo : ISignUpRepo
    {
        private readonly DapperContext _dapperContext;

        public SignUpRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> CreateUserAsync(IUserModel user)
        {
            const string sql = @"
                INSERT INTO Users (Name,Email, PasswordHash, Role) 
                VALUES (@UserName, @Email,@PasswordHash,  @Role);
                SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (var connection = _dapperContext.createConnection())
            {
                int userId = await connection.QuerySingleAsync<int>(sql, new
                {
                    UserName = user.Name,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                   
                    Role = user.Role
                });

                

                return userId; // Returns the generated UserId


            }
        }
    }
}
