using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.Model.Responses
{
    public class LookUpOrderResponse: Response
    {
        public List<Order> Orders { get; set; }
    }
}
