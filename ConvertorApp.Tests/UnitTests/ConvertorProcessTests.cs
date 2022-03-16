using Convertor.API.Contracts;
using Convertor.API.Services.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertorApp.Tests.UnitTests
{
    [TestFixture]
    public class ConvertorProcessTests
    {
        private IConvertProcess _convertProcess;

        [SetUp]
        public void SetUp()
        {
            _convertProcess = new ConvertProcess();
        }

        [Test]
        [TestCase("0", "zero dollars")]
        [TestCase("1", "one dollar")]
        [TestCase("25,1", "twenty-five dollars and ten cents")]
        [TestCase("0,01", "zero dollars and one cent")]
        [TestCase("45100", "forty-five thousand one hundred dollars")]
        [TestCase("999999999,99", "nine hundred ninty-nine million nine hundred ninty-nine thousand nine hundred ninty-nine dollars and ninty-nine cents")]
        public void ConsumptionServiceShouldReturnExpectedValues(string number, string wordExpected)
        {
            var result = _convertProcess.ConvertProcessNumber(number);            

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.WordNumber, wordExpected);
        }
    }
}
