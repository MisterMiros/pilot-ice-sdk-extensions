using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ascon.Pilot.SDK.Extensions.DeepCopies;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class Extensions
    {
        public static IObjectsRepository Repository
        {
            get; set;
        }

        public static int Timeout
        {
            get; set;
        }

        public static bool UseDeepCopies
        {
            get; set;
        }

        public static IAttributeFormatParser AttributeFormatParser
        {
            get; set;
        }

        public static I CreateCopy<I>(I original)
            where I : class
        {
            if (UseDeepCopies)
            {
                return DeepCopyFactory.CreateCopy(original);
            }
            else
            {
                return original;
            }
        }

        public static bool Start(ThreadStart action)
        {         
            if (Repository != null)
            {
                Thread thread = new Thread(action);
                thread.Name = "Extensions";
                thread.Start();
                return true;
            }
            return false;
        }
    }
}
