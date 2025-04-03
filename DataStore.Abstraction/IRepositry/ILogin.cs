using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowdfunding.DTO;
using Datastore.implementation.Models;
using DataStore.Abstraction.AbstractionModel;


namespace DataStore.Abstraction.Repository
{
    public interface ILogin
    {
        Task<IUserModel> LoginDetail(string Email);

    }
}
