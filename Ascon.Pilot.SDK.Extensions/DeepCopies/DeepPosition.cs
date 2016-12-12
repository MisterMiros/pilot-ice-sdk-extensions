using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepPosition : DeepCopy<IPosition>, IPosition
    {
        private DeepPosition(IPosition original) : base(original)
        {
            Order = original.Order;
            Position = original.Position;
        }
        public static IPosition CreateCopy(IPosition original)
        {
            if (original == null || original is DeepCopy<IPosition>)
            {
                return original;
            }
            return new DeepPosition(original);
        }

        public int Order
        {
            get; private set;
        }

        public int Position
        {
            get; private set;
        }
    }
}
