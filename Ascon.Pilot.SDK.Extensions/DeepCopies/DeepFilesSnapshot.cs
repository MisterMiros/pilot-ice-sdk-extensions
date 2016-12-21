using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepFilesSnapshot : DeepCopy<IFilesSnapshot>, IFilesSnapshot
    {
        private DeepFilesSnapshot(IFilesSnapshot original) : base(original) { }

        [DeepCopyCreator(typeof(IFilesSnapshot))]
        public static IFilesSnapshot CreateCopy(IFilesSnapshot original)
        {
            return IsCopy(original) ? original : new DeepFilesSnapshot(original);
        }

        public DateTime Created
        {
            get; private set;
        }

        public int CreatorId
        {
            get; private set;
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
                _files = value == null ? null :
                    new ReadOnlyCollection<IFile>(value.Select(file => file.Copy()).ToArray());
            }
        }

        string _reason;
        public string Reason
        {
            get
            {
                return _reason;
            }
            private set
            {
                _reason = value == null ? null : string.Copy(value);
            }
        }
    }

    public static class IFilesSnapshotDeepCopyExtension
    {
        public static IFilesSnapshot Copy(this IFilesSnapshot original)
        {
            return DeepFilesSnapshot.CreateCopy(original);
        }
    }
}
