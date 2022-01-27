using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.BLL
{
    public class OrderDateNotInFutureException: Exception
    {
        public OrderDateNotInFutureException()
        {

        }
        public OrderDateNotInFutureException(string message) : base(String.Format("Order date must be in the future.: {0}", message))
        {

        }
        public OrderDateNotInFutureException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
