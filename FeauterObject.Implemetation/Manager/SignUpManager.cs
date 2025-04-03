using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowdfunding.Services;
using Datastore.implementation.Models;
using DataStore.Abstraction.AbstractionModel;
using DataStore.Abstraction.IRepositry;
using DataStore.Abstraction.Models;
using FeauterObject.abstraction.Manager;
using FeauterObject.abstraction.Services;
using Org.BouncyCastle.Crypto.Generators;

namespace FeauterObject.Implemetation.Manager
{
    public class SignUpManager:ISignUpManager
    {
        private readonly ISignUpRepo _repo;
        private readonly IPasswordHasher _Hash;
        private readonly ITokenService _services;


        public SignUpManager(ISignUpRepo repo,IPasswordHasher Hash,ITokenService services)
        {
            _repo = repo;
            _Hash = Hash;
            _services = services;
            
        }

        public async Task<string> SignInUser(IUserModel model)

        {
            
            model.PasswordHash=_Hash.HashPasswords(model.PasswordHash);

           var userid= await _repo.CreateUserAsync(model);

            model.UserId = userid;


            var tok = _services.GenerateJwtToken(model);
            return tok;



        }
    }
}