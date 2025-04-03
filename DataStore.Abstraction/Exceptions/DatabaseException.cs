using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.Exceptions
{
   



        public class DatabaseException : Exception
        {

            public DatabaseException(string message) : base(message)
            {

            }

            public DatabaseException(string message, Exception innerException) : base(message, innerException)
            {

            }
        }
    }

