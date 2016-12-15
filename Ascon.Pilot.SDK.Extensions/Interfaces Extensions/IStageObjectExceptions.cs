using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IStageObjectExceptions
    {
        public static IDataObject ToDataObject(this IStageObject stage)
        {
            return Extensions.Repository.Get<IDataObject>(stage.Id);
        }
    }
}
