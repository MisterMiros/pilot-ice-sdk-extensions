using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    class DeepTaskObject : DeepCopy<ITaskObject>, ITaskObject
    {
        private DeepTaskObject() { }
        private DeepTaskObject(ITaskObject original) : base(original)
        {
            Id = original.Id;
            DisplayTitle = original.DisplayTitle + string.Empty;
            DateOfAssignment = original.DateOfAssignment;
            DeadlineDate = original.DeadlineDate;
            State = original.State;
            Executor = DeepPerson.CreateCopy(original.Executor);
            Initiator = DeepPerson.CreateCopy(original.Initiator);
            ExecutorAttachments = new ReadOnlyCollection<Guid>(original.ExecutorAttachments.ToList());
            InitiatorAttachments = new ReadOnlyCollection<Guid>(original.InitiatorAttachments.ToList());
            IsVersion = original.IsVersion;
            Description = original.Description + string.Empty;
            Created = original.Created;
            DateOfCompletion = original.DateOfCompletion;
            DateOfStart = original.DateOfStart;
            ChatId = original.ChatId;
            IsAgreementTask = original.IsAgreementTask;
            Title = original.Title + string.Empty;
            DataState = original.DataState;
            SynchronizationState = original.SynchronizationState;
            Attributes = new Dictionary<string, object>(original.Attributes);
        }

        public static new ITaskObject CreateCopy(ITaskObject original)
        {
            if (original == null || original is DeepCopy<ITaskObject>)
            {
                return original;
            }
            return new DeepTaskObject(original);
        }

        public bool Equals(ITaskObject taskObject)
        {
            return Id == taskObject.Id;
        }

        public Guid Id
        {
            get; private set;
        }

        public string DisplayTitle
        {
            get; private set;
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

        public IPerson Executor
        {
            get; private set;
        }

        public IPerson Initiator
        {
            get; private set;
        }

        public ReadOnlyCollection<Guid> InitiatorAttachments
        {
            get; private set;
        }

        public ReadOnlyCollection<Guid> ExecutorAttachments
        {
            get; private set;
        }

        public bool IsVersion
        {
            get; private set;
        }

        public string Description
        {
            get; private set;
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

        public string Title
        {
            get; private set;
        }

        public IDictionary<string, object> Attributes
        {
            get; private set;
        }
    }
}
