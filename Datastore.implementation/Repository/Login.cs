using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowdfunding.DTO;
using Dapper;
using Datastore.implementation.Models;
using DataStore.Abstraction.AbstractionModel;
using DataStore.Abstraction.Models;
using DataStore.Abstraction.Repository;

namespace Datastore.implementation.Repository
{
    public class Login : ILogin
    {
        private readonly DapperContext _dapper;

        public Login(DapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<IUserModel> LoginDetail( string Email)
        {
            string sql = "SELECT * FROM Users WHERE Email=@Email";

            using (var connection = _dapper.createConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<UserModel>(sql, new { Email=Email });
            }
        }


    }
}
