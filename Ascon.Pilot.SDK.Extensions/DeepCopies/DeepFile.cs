using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepFile : DeepCopy<IFile>, IFile
    {
        private DeepFile(IFile original) : base(original)
        {
            Accessed = original.Accessed;
            Created = original.Created;
            Id = original.Id;
            Md5 = original.Md5;
            Modified = original.Modified;
            Name = original.Name;
            Signatures = original.Signatures;
            Size = original.Size;
        }

        public static IFile CreateCopy(IFile original)
        {
            if (original == null || original is DeepCopy<IFile>)
            {
                return original;
            }
            return new DeepFile(original);
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
}
