using AccountPlugin.EntityNS;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPlugin.EntityNS
{
    public class Account : EntityBase
    {
        public Account(IOrganizationService service, IPluginExecutionContext context) : base(service, context){

        }
        public override void Create()
        {

            if (_context.InputParameters.Contains("Target") && _context.InputParameters["Target"] is Entity)
            {
                var target = (Entity)_context.InputParameters["Target"];

                OrganizationServiceContext orgServiceContext = new OrganizationServiceContext(_service);
                var existingAccount = (from accountEntity in orgServiceContext.CreateQuery("account")
                                       where ((string)accountEntity["name"]).Equals(target["name"])
                                       select accountEntity).FirstOrDefault();
                if (existingAccount != null)
                    return;

                var contact = new Entity("contact");
                contact["parentaccount"] = new EntityReference("account", target.Id);
                _service.Create(contact);
            }
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
