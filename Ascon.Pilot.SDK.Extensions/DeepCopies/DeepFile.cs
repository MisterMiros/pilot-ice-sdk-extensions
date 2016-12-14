using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepFile : DeepCopy<IFile>, IFile
    {
        private DeepFile(IFile original) : base(original) { }

        public static IFile CreateCopy(IFile original)
        {
            return IsCopy(original) ? original : new DeepFile(original);
        }

        public DateTime Accessed
        {
            get; private set;
        }

        public DateTime Created
        {
            get; private set;
        }

        public Guid Id
        {
            get; private set;
        }


        string _md5;
        public string Md5
        {
            get
            {
                return _md5;
            }
            private set
            {
                _md5 = string.Copy(value ?? string.Empty);
            }
        }

        public DateTime Modified
        {
            get; private set;
        }


        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = string.Copy(value ?? string.Empty);
            }
        }

        ReadOnlyCollection<ISignature> _signatures;
        public ReadOnlyCollection<ISignature> Signatures
        {
            get
            {
                return _signatures;
            }
            private set
            {
                _signatures =
                    new ReadOnlyCollection<ISignature>(value.Select(sign => DeepSignature.CreateCopy(sign)).ToArray());
            }
        }

        public long Size
        {
            get; private set;
        }
    }

    public static class IFileDeepCopyExtension
    {
        public static IFile Copy(this IFile original)
        {
            return DeepFile.CreateCopy(original);
        }
    }
}
