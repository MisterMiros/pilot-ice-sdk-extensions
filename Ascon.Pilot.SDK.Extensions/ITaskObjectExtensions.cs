using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ITaskObjectExtensions
    {
        public static IDataObject ToDataObject(this ITaskObject task)
        {
            return Extensions.Repository.Get<IDataObject>(task.Id);
        }
    }
}
