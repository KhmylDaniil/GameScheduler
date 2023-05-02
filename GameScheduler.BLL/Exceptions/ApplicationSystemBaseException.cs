using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScheduler.BLL.Exceptions
{
    public class ApplicationSystemBaseException : Exception
    {
        public ApplicationSystemBaseException(string message) : base(message)
        {
        }
    }
}
