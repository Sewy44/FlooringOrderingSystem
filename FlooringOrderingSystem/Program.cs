using FlooringOrderingSystem.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderController manager = new OrderController();
            manager.Run();
        }
    }
}
