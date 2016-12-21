using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepTaskObject : DeepCopy<ITaskObject>, ITaskObject
    {
        private DeepTaskObject(ITaskObject original) : base(original) { }

        [DeepCopyCreator(typeof(ITaskObject))]
        public static ITaskObject CreateCopy(ITaskObject original)
        {
            return IsCopy(original) ? original : new DeepTaskObject(original);
        }

        public Guid Id
        {
            get; private set;
        }

        string _displayTitle;
        public string DisplayTitle
        {
            get
            {
                return _displayTitle;
            }
            private set
            {
                _displayTitle = value == null ? null : string.Copy(value);
            }
        }

        public DateTime DateOfAssignment
        {
            get; private set;
        }

        public DateTime DeadlineDate
        {
            get; private set;
        }

        public State State
        {
            get; private set;
        }

        IPerson _executor;
        public IPerson Executor
        {
            get
            {
                return _executor;
            }
            private set
            {
                _executor = value.Copy();
            }
        }

        IPerson _initiator;
        public IPerson Initiator
        {
            get
            {
                return _initiator;
            }
            private set
            {
                _initiator = value.Copy();
            }
        }

        ReadOnlyCollection<Guid> _initiatorAttachments;
        public ReadOnlyCollection<Guid> InitiatorAttachments
        {
            get
            {
                return _initiatorAttachments;
            }
            private set
            {
                _initiatorAttachments = value == null ? null : new ReadOnlyCollection<Guid>(value);
            }
        }

        ReadOnlyCollection<Guid> _executorAttachments;
        public ReadOnlyCollection<Guid> ExecutorAttachments
        {
            get
            {
                return _executorAttachments;
            }
            private set
            {
                _executorAttachments = value == null ? null : new ReadOnlyCollection<Guid>(value);
            }
        }

        public bool IsVersion
        {
            get; private set;
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

        public DateTime Created
        {
            get; private set;
        }

        public DateTime? DateOfCompletion
        {
            get; private set;
        }

        public DateTime? DateOfStart
        {
            get; private set;
        }

        public Guid ChatId
        {
            get; private set;
        }

        public DataState DataState
        {
            get; private set;
        }

        public bool IsAgreementTask
        {
            get; private set;
        }

        public SynchronizationState SynchronizationState
        {
            get; private set;
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

        IDictionary<string, object> _attributes;
        public IDictionary<string, object> Attributes
        {
            get
            {
                return _attributes;
            }
            private set
            {
                _attributes = value.ToDictionary(
                    kv => string.Copy(kv.Key ?? string.Empty),
                    kv =>
                    {
                        if (kv.Value is string)
                        {
                            return string.Copy(kv.Value as string);
                        }
                        else
                        {
                            return kv.Value;
                        }
                    });
            }
        }
    }

    public static class ITaskObjectDeepCopyExtension
    {
        public static ITaskObject Copy(this ITaskObject original)
        {
            return DeepTaskObject.CreateCopy(original);
        }
    }
}
