using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.BLL
{
    public class InvalidDecimalException:Exception
    {
        public InvalidDecimalException()
        {

        }
        public InvalidDecimalException(string message) : base(String.Format("Invalid Area: {0}", message))
        {

        }
        public InvalidDecimalException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
