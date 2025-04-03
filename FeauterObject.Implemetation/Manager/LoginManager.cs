using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DataStore.Abstraction.Models;
using DataStore.Abstraction.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using FeauterObject.abstraction.Manager;
using Datastore.implementation.Models;
using FeauterObject.abstraction.Services;


namespace FeauterObject.Implemetation.Manager
{
    public class LoginManager : ILoginManager
    {
        private readonly ILogin _repository;
        private readonly IConfiguration _config;
        private readonly ITokenService _service;

        public LoginManager(ILogin repository,IConfiguration config,ITokenService servicer)
        {
            _repository = repository;
            _config = config;
            _service = servicer;
        }

        public async  Task<string> ValidateUser( string password,string Email)
            {
            var user_details = await _repository.LoginDetail( Email);
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user_details.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }
           var tok= _service.GenerateJwtToken(user_details);
            return tok;
        }


    }
}
