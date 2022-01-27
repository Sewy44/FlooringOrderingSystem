using FlooringOrderingSystem.Data.Interfaces;
using FlooringOrderingSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlooringOrderingSystem.Data.Repositories
{
    public class TaxRepository:ITaxRepository
    {
        private Dictionary<string, Tax> _taxes = new Dictionary<string, Tax>();
        private static Tax _tax = new Tax();
        private string taxFileName = ConfigurationManager.AppSettings["taxFileName"].ToString();

        public List<Tax> ReadAllTaxes()
        {//read the tax file. Store the result in a List and return the list. Stores taxes in memory
            _taxes = new Dictionary<string, Tax>();

            string[] rows = File.ReadAllLines(taxFileName);

            for (int i = 1; i < rows.Length; i++)
            {
                _tax = UnMarshallTax(rows[i]);
                _taxes.Add(_tax.StateAbbreviation, _tax);
            }

            return _taxes.Values.ToList();
        }

        public Tax ReadTaxesByState(string stateAbbreviation)
        {
           List<Tax> taxList = ReadAllTaxes();

            return _taxes[stateAbbreviation];
        }
        private Tax UnMarshallTax(string taxString)
        {
            string[] taxElements = taxString.Split(',');

            Tax tax = new Tax();

            tax.StateAbbreviation = taxElements[0];
            tax.StateName = taxElements[1];
            tax.TaxRate = Convert.ToDecimal(taxElements[2]);

            return tax;
        }

    }
}
