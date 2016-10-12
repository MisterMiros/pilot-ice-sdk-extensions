using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepDataObject : DeepCopy<IDataObject>, IDataObject
    {
        private DeepDataObject() { }
        private DeepDataObject(IDataObject original) : base(original)
        {
            Id = original.Id;
            ParentId = original.ParentId;
            Type = DeepType.CreateCopy(original.Type);
            Creator = DeepPerson.CreateCopy(original.Creator);
            Children = new ReadOnlyCollection<Guid>(original.Children.ToArray());
            DisplayName = original.DisplayName + string.Empty;
            Created = original.Created;
            IsDeleted = original.IsDeleted;
            IsInRecycleBin = original.IsInRecycleBin;
            IsSecret = original.IsSecret;
            State = original.State;
            Attributes = new Dictionary<string, object>(original.Attributes);
            TypesByChildren = new Dictionary<Guid, int>(original.TypesByChildren);
        }

        public static IDataObject CreateCopy(IDataObject original)
        {
            if (original == null || original is DeepCopy<IDataObject>)
            {
                return original;
            }
            return new DeepDataObject(original);
        }

        public bool Equals(IDataObject dataObject)
        {
            return Id == dataObject.Id;
        }

        public Guid Id
        {
            get; private set;
        }

        public Guid ParentId
        {
            get; private set;
        }

        public IPerson Creator
        {
            get; private set;
        }

        public IType Type
        {
            get; private set;
        }

        public ReadOnlyCollection<Guid> Children
        {
            get; private set;
        }

        public string DisplayName
        {
            get; private set;
        }

        public DateTime Created
        {
            get; private set;
        }

        public bool IsDeleted
        {
            get; private set;
        }

        public bool IsInRecycleBin
        {
            get; private set;
        }

        public bool IsSecret
        {
            get; private set;
        }

        public DataState State
        {
            get; private set;
        }

        public IDictionary<string, object> Attributes
        {
            get; private set;
        }

        public IDictionary<Guid, int> TypesByChildren
        {
            get; private set;
        }


        public IDictionary<int, IAccess> Access
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IFilesSnapshot ActualFileSnapshot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<IFile> Files
        {
            get
            {
                throw new NotImplementedException();
            }
        }      

        public ReadOnlyCollection<IFilesSnapshot> PreviousFileSnapshots
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<Guid> RelatedSourceFiles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<Guid> RelatedTaskInitiatorAttachments
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<Guid> RelatedTaskMessageAttachments
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public SynchronizationState SynchronizationState
        {
            get
            {
                throw new NotImplementedException();
            }
        }  

        public ReadOnlyCollection<int> Subscribers
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ILockInfo LockInfo
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
