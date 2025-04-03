using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.AbstractionModel
{
    public interface IUserModel
    {

         string Name { get; set; }
         string PasswordHash { get; set; }
         int UserId { get; set; }

         string Email { get; set; }

         string Role { get; set; }



    }
}
