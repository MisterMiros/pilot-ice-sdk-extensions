using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepOrganisationUnit : DeepCopy<IOrganisationUnit>, IOrganisationUnit
    {
        private DeepOrganisationUnit(IOrganisationUnit original) : base(original) { }

        [DeepCopyCreator(typeof(IOrganisationUnit))]
        public static IOrganisationUnit CreateCopy(IOrganisationUnit original)
        {
            return IsCopy(original) ? original : new DeepOrganisationUnit(original);
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

    public static class IOrganisationUnitDeepCopyExtension
    {
        public static IOrganisationUnit Copy(this IOrganisationUnit original)
        {
            return DeepOrganisationUnit.CreateCopy(original);
        }
    }
}
