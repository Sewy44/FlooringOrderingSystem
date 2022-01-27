using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderingSystem.Data.Repositories;
using NUnit.Framework;
using FlooringOrderingSystem.Model;
using System.Configuration.Assemblies;
using System.IO;

namespace FlooringOrderingSystem.Tests
{
    [TestFixture]
    public class TaxRepositoryTests
    {
        TaxRepository taxRepository = new TaxRepository();
       
        [Test]
        public void ReadAllTaxes_CanReadAllTaxes()
        {
            var taxResult = taxRepository.ReadAllTaxes();
            Assert.IsNotNull(taxResult);
        }

        [TestCase("OH")]
        [TestCase("MI")]
        [TestCase("IN")]
        [TestCase("PA")]
        [Test]
        public void ReadTaxesByState_CanReadTaxesByState(string stateAbbeviation)
        {
            Tax stateReturned = taxRepository.ReadTaxesByState(stateAbbeviation);
            Assert.AreEqual(stateAbbeviation, stateReturned.StateAbbreviation);
        }   
    }
}
