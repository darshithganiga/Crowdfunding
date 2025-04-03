using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastore.implementation.Models;
using DataStore.Abstraction.AbstractionModel;

namespace FeauterObject.abstraction.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(IUserModel user);

    }
}
