using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class Utilities
    {
        public static string FormatFromDictionary(this string formatString, IDictionary<string, object> ValueDict)
        {
            int i = 0;
            StringBuilder newFormatString = new StringBuilder(formatString);
            IDictionary<string, int> keyToInt = new Dictionary<string, int>();
            foreach (var tuple in ValueDict)
            {
                newFormatString = newFormatString.Replace("{" + tuple.Key + "}", "{" + i.ToString() + "}");
                keyToInt.Add(tuple.Key, i);
                i++;
            }
            return String.Format(newFormatString.ToString(), ValueDict.OrderBy(x => keyToInt[x.Key]).Select(x => x.Value.ToString()).ToArray());
        }

        public static Guid[] ToArray(this Guid guid)
        {
            return new Guid[] { guid };
        }
    }
}
