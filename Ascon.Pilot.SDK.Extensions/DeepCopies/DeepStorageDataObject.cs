using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepStorageDataObject : DeepCopy<IStorageDataObject>, IStorageDataObject
    {
        private DeepStorageDataObject(IStorageDataObject original) : base(original) { }

        [DeepCopyCreator(typeof(IStorageDataObject))]
        public static IStorageDataObject CreateCopy(IStorageDataObject original)
        {
            return IsCopy(original) ? original : new DeepStorageDataObject(original);
        }

        IDataObject _dataObject;
        public IDataObject DataObject
        {
            get
            {
                return _dataObject;
            }
            private set
            {
                _dataObject = value.Copy();
            }
        }

        public bool IsDirectory
        {
            get; private set;
        }

        string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            private set
            {
                _path = string.Copy(value ?? string.Empty);
            }
        }

        public StorageObjectState State
        {
            get; private set;
        }
    }

    public static class IStorageDataObjectDeepCopyExtension
    {
        public static IStorageDataObject Copy(this IStorageDataObject original)
        {
            return DeepStorageDataObject.CreateCopy(original);
        }
    }
}
