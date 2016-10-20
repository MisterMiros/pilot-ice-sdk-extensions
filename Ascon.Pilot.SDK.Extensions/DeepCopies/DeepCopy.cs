using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public abstract class DeepCopy<I>
    {
        protected DeepCopy() { }
        protected DeepCopy(I original)
        {
        }
    }

    public static class DeepCopyFactory
    {
        public static I CreateCopy<I>(I original)
            where I : class
        {
            if (typeof(I) == typeof(IDataObject))
            {
                return DeepDataObject.CreateCopy(original as IDataObject) as I;
            }
            if (typeof(I) == typeof(ITaskObject))
            {
                return DeepTaskObject.CreateCopy(original as ITaskObject) as I;
            }
            if (typeof(I) == typeof(IType))
            {
                return DeepType.CreateCopy(original as IType) as I;
            }
            if (typeof(I) == typeof(IPosition))
            {
                return DeepPosition.CreateCopy(original as IPosition) as I;
            }
            if (typeof(I) == typeof(IPerson))
            {
                return DeepPerson.CreateCopy(original as IPerson) as I;
            }
            if (typeof(I) == typeof(IAttribute))
            {
                return DeepAttribute.CreateCopy(original as IAttribute) as I;
            }
            return original;
        }
    }
}
