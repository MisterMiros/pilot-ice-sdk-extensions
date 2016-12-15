using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepAccess : DeepCopy<IAccess>, IAccess
    {
        private DeepAccess(IAccess original) : base(original) { }

        [DeepCopyCreator(typeof(IAccess))]
        public static IAccess CreateCopy(IAccess original)
        {
            return IsCopy(original) ? original : new DeepAccess(original);
        }

        public AccessLevel AccessLevel
        {
            get; private set;
        }

        public bool IsInheritable
        {
            get; private set;
        }

        public bool IsInherited
        {
            get; private set;
        }

        public DateTime ValidThrough
        {
            get; private set;
        }
    }

    public static class IAccessDeepCopyExtension
    {
        public static IAccess Copy(this IAccess original)
        {
            return DeepAccess.CreateCopy(original);
        }
    }
}
