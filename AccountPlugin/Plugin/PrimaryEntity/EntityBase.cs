using AccountPlugin.Enum;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPlugin.Plugin.PrimaryEntity
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
                case SdkMessage.Create:
                    executeLogicBasedOnStageName(null, null, CreatePostOperation);
                    break;
                case SdkMessage.Update:
                    executeLogicBasedOnStageName(UpdatePreValidation, UpdatePreOperation, UpdatePostOperation);
                    break;
                case SdkMessage.Delete:
                    executeLogicBasedOnStageName(null, DeletePreOperation, null);
                    break;
                default:
                    break;
            }


        }
        private delegate void preValidationLogic();
        private delegate void preOperationLogic();
        private delegate void postOperationLogic();

        private void executeLogicBasedOnStageName(
            preValidationLogic preValidationLogic = null, preOperationLogic preOperationLogic = null, postOperationLogic postOperationLogic = null)
        {
            if (_context.Stage == (int)StageName.PreValidation)
            {
                //kan ha delegate function som gör vissa boilerplate kollar för en viss typ av operation och därefter exekverar preValidation inne i den.
                preValidationLogic();
            }
            else if (_context.Stage == (int)StageName.PreOperation)
            {
                preOperationLogic();
            }
            else if (_context.Stage == (int)StageName.PostOperation)
            {
                postOperationLogic();
            }
        }

        public abstract void CreatePostOperation();

        public abstract void UpdatePostOperation();
        public virtual void UpdatePreValidation()
        {

        }
        public virtual void UpdatePreOperation()
        {

        }

        public abstract void DeletePreOperation();

        //these are optional to implement in derivate
        public virtual void SetStatePostOperation()
        {

        }
    }
}
