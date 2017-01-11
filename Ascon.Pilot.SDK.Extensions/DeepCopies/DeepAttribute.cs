using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepAttribute : DeepCopy<IAttribute>, IAttribute
    {
        private DeepAttribute(IAttribute original) : base(original) { }

        [DeepCopyCreator(typeof(IAttribute))]
        public static IAttribute CreateCopy(IAttribute original)
        {
            return IsCopy(original) ? original : new DeepAttribute(original);
        }


        string _configuration;
        public string Configuration
        {
            get
            {
                return _configuration;
            }
            private set
            {
                _configuration = CopyString(value);
            }
        }

        public bool IsObligatory
        {
            get; private set;
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
                _name = CopyString(value);
            }
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
                _title = CopyString(value);
            }
        }

        public int DisplayHeight
        {
            get; private set;
        }

        public int DisplaySortOrder
        {
            get; private set;
        }

        public bool ShowInObjectsExplorer
        {
            get; private set;
        }

        public AttributeType Type
        {
            get; private set;
        }
    }

    public static class IAttributeDeepCopyExtension
    {
        public static IAttribute Copy(this IAttribute original)
        {
            return DeepAttribute.CreateCopy(original);
        }
    }
}
