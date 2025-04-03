using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastore.implementation.Models;
using DataStore.Abstraction.AbstractionModel;
using DataStore.Abstraction.Models;

namespace FeauterObject.abstraction.Manager
{
    public interface ISignUpManager
    {
        Task <string> SignInUser(IUserModel model);

    }
}
