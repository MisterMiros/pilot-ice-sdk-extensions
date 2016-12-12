using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepOrganisationUnit : DeepCopy<IOrganisationUnit>, IOrganisationUnit
    {
        private DeepOrganisationUnit(IOrganisationUnit original) : base(original)
        {
            Children = original.Children;
            Id = original.Id;
            IsDeleted = original.IsDeleted;
            IsPosition = original.IsPosition;
            Title = original.Title;
        }

        public static IOrganisationUnit CreateCopy(IOrganisationUnit original)
        {
            if (original == null || original is DeepCopy<IOrganisationUnit>)
            {
                return original;
            }
            return new DeepOrganisationUnit(original);
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

        public int Id
        {
            get; private set;
        }

        public bool IsDeleted
        {
            get; private set;
        }

        public bool IsPosition
        {
            get; private set;
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
    }
}
