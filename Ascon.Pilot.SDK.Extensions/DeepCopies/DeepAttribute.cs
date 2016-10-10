using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepAttribute : DeepCopy<IAttribute>, IAttribute
    {
        private DeepAttribute(IAttribute original)
        {
            Configuration = string.Copy(original.Configuration);
            IsObligatory = original.IsObligatory;
            IsService = original.IsService;
            Name = string.Copy(original.Name);
            Title = string.Copy(original.Title);

        }

        public static IAttribute CreateCopy(IAttribute original)
        {
            if (original == null || original is DeepAttribute)
            {
                return original;
            }
            return new DeepAttribute(original);
        }

        public string Configuration
        {
            get; private set;
        }

        public bool IsObligatory
        {
            get; private set;
        }

        public bool IsService
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public string Title
        {
            get; private set;
        }

        public int DisplayHeight
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int DisplaySortOrder
        {
            get
            {
                throw new NotImplementedException();
            }
        }            

        public bool ShowInObjectsExplorer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public AttributeType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
