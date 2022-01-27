using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.BLL
{
    public class DataDoesNotExistException:Exception
    {
        public DataDoesNotExistException()
        {

        }
        public DataDoesNotExistException(string message) : base(String.Format("Data does not exist: {0}", message))
        {

        }
        public DataDoesNotExistException(string message, Exception inner):base(message, inner)
        {

        }
    }
}
