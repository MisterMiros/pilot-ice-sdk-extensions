using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepType : DeepCopy<IType>, IType
    {
        private DeepType() { }
        private DeepType(IType original) : base(original)
        {
            Name = original.Name + string.Empty;
            Title = original.Title + string.Empty;
            IsService = original.IsService;
            Id = original.Id;
            Attributes = new ReadOnlyCollection<IAttribute>(original.Attributes.ToArray());
            Children = new ReadOnlyCollection<int>(original.Children.ToArray());
            IsDeleted = original.IsDeleted;
        }

        public string Title
        {
            get; private set;
        }

        public bool IsService
        {
            get; private set;
        }

        public static new IType CreateCopy(IType original)
        {
            if (original == null || original is DeepCopy<IType>)
            {
                return original;
            }
            return new DeepType(original);
        }

        public bool Equals(IType type)
        {
            return Id == type.Id;
        }

        public string Name
        {
            get; private set;
        }

        public int Id
        {
            get; private set;
        }

        public ReadOnlyCollection<IAttribute> Attributes
        {
            get; private set;
        }

        public ReadOnlyCollection<int> Children
        {
            get; private set;
        }

        public bool IsDeleted
        {
            get; private set;
        }

        public IEnumerable<IAttribute> DisplayAttributes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool HasFiles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsMountable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public TypeKind Kind
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Sort
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public byte[] SvgIcon
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
