using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ITaskBuilderExtensions
    {
        public static ITaskBuilder AddInitiatorAttachment(this ITaskBuilder builder, ITaskObject task, Guid id)
        {
            IList<Guid> attachments;
            if (task.InitiatorAttachments != null)
            {
               attachments = task.InitiatorAttachments.ToList();
               if (attachments.Contains(id)) { return builder; }
                attachments.Add(id);
            }
            else
            {
                attachments = new Guid[] { id };
            }
            return builder.SetInitiatorAttachments(attachments);
        }

        public static ITaskBuilder AddExecutorAttachment(this ITaskBuilder builder, ITaskObject task, Guid id)
        {
            IList<Guid> attachments;
            if (task.ExecutorAttachments != null)
            {
                attachments = task.ExecutorAttachments.ToList();
                if (attachments.Contains(id)) { return builder; }
                attachments.Add(id);
            }
            else
            {
                attachments = new Guid[] { id };
            }
            return builder.SetExecutorAttachments(attachments);
        }
    }
}
