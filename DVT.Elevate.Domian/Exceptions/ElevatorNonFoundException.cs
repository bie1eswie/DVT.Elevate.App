using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Domian.Exceptions
{
    public class ElevatorNonFoundException : Exception
    {
        public ElevatorNonFoundException(string message)
            : base(message)
        {
        }

    }
}
