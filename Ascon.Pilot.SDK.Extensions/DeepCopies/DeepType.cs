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
        private DeepType(IType original) : base(original) { }

        public static IType CreateCopy(IType original)
        {
            return IsCopy(original) ? original : new DeepType(original);
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
                _title = string.Copy(value ?? string.Empty);
            }
        }

        public bool IsService
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

        public int Id
        {
            get; private set;
        }

        ReadOnlyCollection<IAttribute> _attributes;
        public ReadOnlyCollection<IAttribute> Attributes
        {
            get
            {
                return _attributes;
            }
            private set
            {
                _attributes = new ReadOnlyCollection<IAttribute>(value.Select(attr => DeepAttribute.CreateCopy(attr)).ToArray());
            }
        }

        ReadOnlyCollection<int> _children;
        public ReadOnlyCollection<int> Children
        {
            get
            {
                return _children;
            }
            private set
            {
                _children = new ReadOnlyCollection<int>(value);
            }
        }

        public bool IsDeleted
        {
            get; private set;
        }

        IEnumerable<IAttribute> _displayAttributes;
        public IEnumerable<IAttribute> DisplayAttributes
        {
            get
            {
                return _displayAttributes;
            }
            private set
            {
                _displayAttributes = value.Select(attr => DeepAttribute.CreateCopy(attr)).ToArray();
            }
        }

        public bool HasFiles
        {
            get; private set;
        }

        public bool IsMountable
        {
            get; private set;
        }

        public TypeKind Kind
        {
            get; private set;
        }

        public int Sort
        {
            get; private set;
        }

        byte[] _svgIcon;
        public byte[] SvgIcon
        {
            get
            {
                return _svgIcon;
            }
            private set
            {
                _svgIcon = value?.Select(b => b).ToArray();
            }
        }
    }
}
