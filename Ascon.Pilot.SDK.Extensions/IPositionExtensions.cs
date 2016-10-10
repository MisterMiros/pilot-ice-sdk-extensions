using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IPositionExtensions
    {
        public static IOrganisationUnit GetOrgUnit(this IPosition position)
        {
            return Extensions.Repository.GetOrganisationUnit(position.Position);
        }
    }
}
