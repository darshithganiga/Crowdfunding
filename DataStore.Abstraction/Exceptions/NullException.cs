using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.Exceptions
{
    public class NullException:Exception
    {

        public NullException(string message) : base(message) { }

        public NullException(string message, Exception innerException)
            : base(message, innerException) { }


    }
}
