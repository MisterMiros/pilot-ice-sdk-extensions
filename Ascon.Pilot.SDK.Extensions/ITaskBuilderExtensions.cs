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
            var attachments = builder.TaskObject.InitiatorAttachments.ToList();
            if (attachments.Contains(id)) { return builder; }
            attachments.Add(id);
            return builder.SetInitiatorAttachments(attachments);
        }

        public static ITaskBuilder AddExecutorAttachment(this ITaskBuilder builder, Guid id)
        {
            var attachments = builder.TaskObject.ExecutorAttachments.ToList();
            if (attachments.Contains(id)) { return builder; }
            attachments.Add(id);
            return builder.SetExecutorAttachments(attachments);
        }
    }
}
