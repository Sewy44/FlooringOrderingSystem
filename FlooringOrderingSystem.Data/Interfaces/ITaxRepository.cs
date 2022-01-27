using FlooringOrderingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.Data.Interfaces
{
    public interface ITaxRepository
    {
        List<Tax> ReadAllTaxes();
        Tax ReadTaxesByState(string stateAbbreviation);
    }
}
