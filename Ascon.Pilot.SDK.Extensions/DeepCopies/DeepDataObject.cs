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
        private DeepDataObject(IDataObject original) : base(original) { }

        public static IDataObject CreateCopy(IDataObject original)
        {
            return IsCopy(original) ? original : new DeepDataObject(original);
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

        IPerson _creator;
        public IPerson Creator
        {
            get
            {
                return _creator;
            }
            private set
            {
                _creator = DeepPerson.CreateCopy(value);
            }
        }

        IType _type;
        public IType Type
        {
            get
            {
                return _type;
            }
            private set
            {
                _type = DeepType.CreateCopy(value);
            }
        }

        ReadOnlyCollection<Guid> _children;
        public ReadOnlyCollection<Guid> Children
        {
            get
            {
                return _children;
            }
            private set
            {
                _children = new ReadOnlyCollection<Guid>(value);
            }
        }

        string _displayName;
        public string DisplayName
        {

            get
            {
                return _displayName;
            }
            private set
            {
                _displayName = string.Copy(value ?? string.Empty);
            }

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

        IDictionary<Guid, int> _typesByChildren;
        public IDictionary<Guid, int> TypesByChildren
        {
            get
            {
                return _typesByChildren;
            }
            private set
            {
                _typesByChildren = new Dictionary<Guid, int>(value);
            }
        }

        IDictionary<int, IAccess> _access;
        public IDictionary<int, IAccess> Access
        {
            get
            {
                return _access;
            }
            private set
            {
                _access = value.ToDictionary(kv => kv.Key, kv => DeepAccess.CreateCopy(kv.Value));
            }
        }

        IFilesSnapshot _actualFileSnapshot;
        public IFilesSnapshot ActualFileSnapshot
        {
            get
            {
                return _actualFileSnapshot;
            }
            private set
            {
                _actualFileSnapshot = DeepFilesSnapshot.CreateCopy(value);
            }
        }

        ReadOnlyCollection<IFile> _files;
        public ReadOnlyCollection<IFile> Files
        {
            get
            {
                return _files;
            }
            private set
            {
                _files = new ReadOnlyCollection<IFile>(value.Select(file => DeepFile.CreateCopy(file)).ToArray());
            }
        }

        ReadOnlyCollection<IFilesSnapshot> _previousFileSnapshots;
        public ReadOnlyCollection<IFilesSnapshot> PreviousFileSnapshots
        {
            get
            {
                return _previousFileSnapshots;
            }
            private set
            {
                _previousFileSnapshots =
                    new ReadOnlyCollection<IFilesSnapshot>(value.Select(snap => DeepFilesSnapshot.CreateCopy(snap)).ToArray());
            }
        }

        ReadOnlyCollection<Guid> _relatedSourceFiles;
        public ReadOnlyCollection<Guid> RelatedSourceFiles
        {
            get
            {
                return _relatedSourceFiles;
            }
            private set
            {
                _relatedSourceFiles = new ReadOnlyCollection<Guid>(value);
            }
        }

        ReadOnlyCollection<Guid> _relatedTaskInitiatorAttachments;
        public ReadOnlyCollection<Guid> RelatedTaskInitiatorAttachments
        {
            get
            {
                return _relatedTaskInitiatorAttachments;
            }
            private set
            {
                _relatedTaskInitiatorAttachments = new ReadOnlyCollection<Guid>(value);
            }
        }

        ReadOnlyCollection<Guid> _relatedTaskMessageAttachments;
        public ReadOnlyCollection<Guid> RelatedTaskMessageAttachments
        {
            get
            {
                return _relatedTaskMessageAttachments;
            }
            private set
            {
                _relatedTaskMessageAttachments = new ReadOnlyCollection<Guid>(value);
            }
        }

        public SynchronizationState SynchronizationState
        {
            get; private set;
        }

        ReadOnlyCollection<int> _subscribers;
        public ReadOnlyCollection<int> Subscribers
        {
            get
            {
                return _subscribers;
            }
            private set
            {
                _subscribers = new ReadOnlyCollection<int>(value);
            }
        }

        ILockInfo _lockInfo;
        public ILockInfo LockInfo
        {
            get
            {
                return _lockInfo;
            }
            private set
            {
                _lockInfo = DeepLockInfo.CreateCopy(value);
            }
        }
    }
}
