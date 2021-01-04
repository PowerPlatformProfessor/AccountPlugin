using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPlugin.Plugin
{
    public class Account : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var serviceFacotry = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = (IOrganizationService)serviceFacotry.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                var target = (Entity)context.InputParameters["Target"];

                OrganizationServiceContext orgServiceContext = new OrganizationServiceContext(service);
                var existingAccount = (from accountEntity in orgServiceContext.CreateQuery("account")
                                      where ((string)accountEntity["name"]).Equals(target["name"])
                                      select accountEntity).FirstOrDefault();
                if (existingAccount != null)
                    return;

                var contact = new Entity("contact");
                contact["parentaccount"] = new EntityReference("account", target.Id);
                service.Create(contact);
            }
        }
    }
}
