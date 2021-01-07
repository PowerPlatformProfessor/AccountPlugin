using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPlugin.Plugin.PluginRegistration
{
    public class Account : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var serviceFacotry = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = (IOrganizationService)serviceFacotry.CreateOrganizationService(context.UserId);

            var account = new PrimaryEntity.Account(service, context);
            account.runLogic();
        }

    }
}
