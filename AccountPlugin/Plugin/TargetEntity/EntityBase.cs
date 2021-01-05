using AccountPlugin.Enum;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPlugin.Plugin.TargetEntity
{
    public abstract class EntityBase
    {
        public IOrganizationService _service;
        public IPluginExecutionContext _context;
        public EntityBase(IOrganizationService service, IPluginExecutionContext context)
        {
            _service = service;
            _context = context;
        }

        public void runLogic()
        {
            switch (_context.MessageName)
            {
                case SdkMessageEnum.Create:
                    Create();
                    break;
                case SdkMessageEnum.Update:
                    Update();
                    break;
                case SdkMessageEnum.Delete:
                    Delete();
                    break;
                default:
                    break;
            }

       
        }
        
        public abstract void Create();
        public abstract void Update();
        public abstract void Delete();

        //these are optional to implement in derivate
        public virtual void SetState() {
        
        }
    }
}
