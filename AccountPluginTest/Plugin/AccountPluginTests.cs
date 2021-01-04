using AccountPlugin.Plugin;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
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
        private readonly Entity _account;
        //private readonly Entity _contact;


        public AccountPluginTests()
        {
            _context = new XrmFakedContext();
            _account = new Entity("account", Guid.NewGuid());
            _account["name"] = "Test";
            //_contact = new Entity("contact", Guid.NewGuid());
        }

        [Fact]
        public void Given_AccountCreated_When_Then_CreateDummyContact()
        {

            _context.ExecutePluginWithTarget<Account>(_account);

            var contact = _context.CreateQuery("contact").FirstOrDefault();

            Assert.NotNull(contact);
            Assert.Equal(contact["parentaccount"], _account.Id);
        }
    }
}
