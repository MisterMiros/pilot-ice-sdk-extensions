using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepWorkflowObject : DeepCopy<IWorkflowObject>, IWorkflowObject
    {
        private DeepWorkflowObject(IWorkflowObject original) : base(original) { }

        [DeepCopyCreator(typeof(IWorkflowObject))]
        public static IWorkflowObject CreateCopy(IWorkflowObject original)
        {
            return IsCopy(original) ? original : new DeepWorkflowObject(original);
        }

        string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            private set
            {
                _description = value == null ? null : string.Copy(value);
            }
        }

        public Guid Id
        {
            get; private set;
        }

        IEnumerable<Guid> _initiatorAttachments;
        public IEnumerable<Guid> InitiatorAttachments
        {
            get
            {
                return _initiatorAttachments;
            }
            private set
            {
                _initiatorAttachments = value?.Select(a => a).ToArray();
            }
        }

        string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            private set
            {
                _title = value == null ? null : string.Copy(value);
            }
        }
    }

    public static class IWorkflowObjectDeepCopyExtension
    {
        public static IWorkflowObject Copy(this IWorkflowObject original)
        {
            return DeepWorkflowObject.CreateCopy(original);
        }
    }
}
