using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ITaskBuilderExtensions
    {
        public static ITaskBuilder AddInitiatorAttachment(this ITaskBuilder builder, Guid id)
        {
            IList<Guid> attachments;
            if (builder.TaskObject.InitiatorAttachments != null)
            {
               attachments = builder.TaskObject.InitiatorAttachments.ToList();
               if (attachments.Contains(id)) { return builder; }
                attachments.Add(id);
            }
            else
            {
                attachments = new Guid[] { id };
            }
            return builder.SetInitiatorAttachments(attachments);
        }

        public static ITaskBuilder AddExecutorAttachment(this ITaskBuilder builder, Guid id)
        {
            IList<Guid> attachments;
            if (builder.TaskObject.ExecutorAttachments != null)
            {
                attachments = builder.TaskObject.ExecutorAttachments.ToList();
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
