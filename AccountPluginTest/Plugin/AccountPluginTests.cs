using AccountPlugin.Plugin;
using AccountPlugin.Plugin.PluginRegistration;
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
        public void Given_When_PluginExecutesCreateAccount_Then_CreateDummyContact()
        {
            //Arrange - can use testData from constructor or specific data defined her under arrange

            //Act
            _context.ExecutePluginWithTarget<Account>(_account);

            var contact = _context.CreateQuery("contact").FirstOrDefault();
            
            //Assert
            Assert.NotNull(contact);
            Assert.Equal(_account.Id, ((EntityReference)contact["parentaccount"]).Id);
        }

        [Fact]
        public void Given_AccountAlreadyExists_When_PluginExecutesCreateAccount_Then_DontCreateContact()
        {
            //Arrange - can use testData from constructor or specific data defined her under arrange
            var existingAccount = new Entity("account", Guid.NewGuid());
            existingAccount["name"] = "Test";
            _context.Initialize(existingAccount);


            //Act
            _context.ExecutePluginWithTarget<Account>(_account);


            //Assert
            var contact = _context.CreateQuery("contact").FirstOrDefault();
            Assert.Null(contact);

        }
    }
}
