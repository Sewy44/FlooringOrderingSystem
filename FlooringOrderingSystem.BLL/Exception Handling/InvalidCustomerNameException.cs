using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.BLL.Exception_Handling
{
    public class InvalidCustomerNameException: Exception
    {
        public InvalidCustomerNameException()
        {

        }
        public InvalidCustomerNameException(string message) : base(String.Format("Customer name invalid {0}", message))
        {

        }
        public InvalidCustomerNameException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
