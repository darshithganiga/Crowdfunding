using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.AbstractionModel;
using DataStore.Abstraction.Models;

namespace Datastore.implementation.Models
{
    public class UserModel:IUserModel
    {

       public string Name { get; set; }
       public string PasswordHash { get; set; }
       public int UserId { get; set; }

       public string Email { get; set; }

       public string Role { get; set; }


    }
}
