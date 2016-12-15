using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IWorkflowObjectExtensions
    {
        public static IDataObject ToDataObject(this IWorkflowObject workflow)
        {
            return Extensions.Repository.Get<IDataObject>(workflow.Id);
        }

        public static IEnumerable<IDataObject> GetInitiatorAttachments(this IWorkflowObject workflow, ObjectGettingExtensions.OnException onException = null)
        {
            return Extensions.Repository.Get<IDataObject>(workflow.InitiatorAttachments, onException);
        }
    }
}
