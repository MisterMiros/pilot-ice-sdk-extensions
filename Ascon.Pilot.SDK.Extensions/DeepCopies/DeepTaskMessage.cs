using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepTaskMessage : DeepCopy<ITaskMessage>, ITaskMessage
    {
        private DeepTaskMessage(ITaskMessage original) : base(original) { }

        [DeepCopyCreator(typeof(ITaskMessage))]
        public static ITaskMessage CreateCopy(ITaskMessage original)
        {
            return IsCopy(original) ? original : new DeepTaskMessage(original);
        }

        IEnumerable<Guid> _attachments;
        public IEnumerable<Guid> Attachments
        {
            get
            {
                return _attachments;
            }
            private set
            {
                _attachments = value?.Select(a => a).ToArray();
            }
        }

        IPerson _author;
        public IPerson Author
        {
            get
            {
                return _author;
            }
            private set
            {
                _author = value.Copy();
            }
        }

        public DateTime Created
        {
            get; private set;
        }

        public DataState DataState
        {
            get; private set;
        }

        public Guid Id
        {
            get; private set;
        }

        public SynchronizationState SynchronizationState
        {
            get; private set;
        }

        string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            private set
            {
                _text = value == null ? null : string.Copy(value);
            }
        }     
    }

    public static class ITaskMessageDeepCopyExtension
    {
        public static ITaskMessage Copy(this ITaskMessage original)
        {
            return DeepTaskMessage.CreateCopy(original);
        }
    }
}
