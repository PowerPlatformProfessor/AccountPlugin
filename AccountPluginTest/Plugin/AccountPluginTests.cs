using FakeXrmEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountPluginTest.Plugin
{
    public class AccountPluginTests
    {
        private readonly XrmFakedContext _context;

        [Fact]
        public void Given_AccountCreated_When_Then_CreateDummyContact()
        {
            Assert.True(false);
        }
    }
}
