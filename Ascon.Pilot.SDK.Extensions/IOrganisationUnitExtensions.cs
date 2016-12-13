using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IOrganisationUnitExtensions
    {
        public static IEnumerable<IOrganisationUnit> GetChildren(this IOrganisationUnit unit)
        {
            foreach (var id in unit.Children)
            {
                yield return Extensions.CreateCopy(Extensions.Repository.GetOrganisationUnit(id));
            }
        }
    }
}
