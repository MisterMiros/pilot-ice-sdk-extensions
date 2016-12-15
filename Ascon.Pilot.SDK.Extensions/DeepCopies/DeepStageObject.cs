using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepStageObject : DeepCopy<IStageObject>, IStageObject
    {
        private DeepStageObject(IStageObject original) : base(original) { }

        [DeepCopyCreator(typeof(IStageObject))]
        public static IStageObject CreateCopy(IStageObject original)
        {
            return IsCopy(original) ? original : new DeepStageObject(original);
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

    public static class IStageObjectDeepCopyExtension
    {
        public static IStageObject Copy(this IStageObject original)
        {
            return DeepStageObject.CreateCopy(original);
        }
    }
}
