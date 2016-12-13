using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepStageObject : DeepCopy<IStageObject>, IStageObject
    {
        private DeepStageObject(IStageObject original) : base(original) { }

        public static IStageObject CreateCopy(IStageObject original)
        {
            if (original == null || original is DeepCopy<IStageObject>)
            {
                return original;
            }
            return new DeepStageObject(original);
        }

        public Guid Id
        {
            get; private set;
        }

        public int Order
        {
            get; private set;
        }
    }
}
