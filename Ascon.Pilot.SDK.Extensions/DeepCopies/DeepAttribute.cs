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
            Configuration = string.Copy(original.Configuration ?? string.Empty);
            IsObligatory = original.IsObligatory;
            IsService = original.IsService;
            Name = string.Copy(original.Name);
            Title = string.Copy(original.Title);
            DisplayHeight = original.DisplayHeight;
            DisplaySortOrder = original.DisplaySortOrder;
            ShowInObjectsExplorer = original.ShowInObjectsExplorer;
            Type = original.Type;
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
}
