using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    class DeepPosition : DeepCopy<IPosition>, IPosition
    {
        private DeepPosition() { }
        private DeepPosition(IPosition original) : base(original)
        {
            Order = original.Order;
            Position = original.Position;
        }
        public static new IPosition CreateCopy(IPosition original)
        {
            if (original == null || original is DeepCopy<IPosition>)
            {
                return original;
            }
            return new DeepPosition(original);
        }

        public bool Equals(IPosition position)
        {
            return Position == position.Position;
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
