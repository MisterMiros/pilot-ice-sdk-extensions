﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepPosition : DeepCopy<IPosition>, IPosition
    {
        private DeepPosition(IPosition original) : base(original) { }
        public static IPosition CreateCopy(IPosition original)
        {
            return IsCopy(original) ? original : new DeepPosition(original);
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
