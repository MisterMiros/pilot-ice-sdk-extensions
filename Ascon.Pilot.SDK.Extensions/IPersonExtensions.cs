using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IPersonExtensions
    {
        public static bool HasPositionWithName(this IPerson person, string position)
        {
            return (from orgUnit in (from pos in person.Positions
                                     select pos.GetOrgUnit())
                    where orgUnit.Title == position
                    select orgUnit).Any();
        }
    }
}
