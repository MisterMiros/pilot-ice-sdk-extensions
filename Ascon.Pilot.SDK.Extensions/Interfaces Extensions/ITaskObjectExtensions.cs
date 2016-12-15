using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ITaskObjectExtensions
    {
        public static IDataObject ToDataObject(this ITaskObject task)
        {
            return Extensions.Repository.Get<IDataObject>(task.Id);
        }

        public static IEnumerable<IDataObject> GetInitiatorAttachments(this ITaskObject task, ObjectGettingExtensions.OnException onException = null)
        {
            return Extensions.Repository.Get<IDataObject>(task.InitiatorAttachments, onException);
        }

        public static IEnumerable<IDataObject> GetExecutorAttachments(this ITaskObject task, ObjectGettingExtensions.OnException onException = null)
        {
            return Extensions.Repository.Get<IDataObject>(task.ExecutorAttachments, onException);
        }

        public static IEnumerable<ITaskObject> GetPreviousVersions(this ITaskObject task)
        {
            IDataObject dataObject = task.ToDataObject();
            foreach (var child in dataObject.GetChildren())
            {
                if (child.Type.Name == SystemTypeNames.TASK)
                {
                    var childTask = child.ToTask();
                    if (childTask.IsVersion) { yield return childTask; }
                }
            }
        }
    }
}
