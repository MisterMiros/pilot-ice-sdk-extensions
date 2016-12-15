using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ITaskMessageExtensions
    {
        public static IDataObject ToDataObject(this ITaskMessage message)
        {
            return Extensions.Repository.Get<IDataObject>(message.Id);
        }
    }
}
