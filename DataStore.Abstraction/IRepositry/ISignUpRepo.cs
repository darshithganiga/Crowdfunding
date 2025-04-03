using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastore.implementation.Models;
using DataStore.Abstraction.AbstractionModel;


namespace DataStore.Abstraction.IRepositry
{
    public interface ISignUpRepo
    {
        Task<int> CreateUserAsync(IUserModel user);
    }
}
