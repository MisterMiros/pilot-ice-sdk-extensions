using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    abstract class DeepCopy<I>
    {
        protected DeepCopy() { }
        protected DeepCopy(I original) { }
        public static I CreateCopy(I original)
        {
            return original;
        }
    }
}
