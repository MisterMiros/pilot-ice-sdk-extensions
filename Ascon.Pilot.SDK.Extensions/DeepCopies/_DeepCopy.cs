using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public abstract class DeepCopy<I>
    {
        protected DeepCopy(I original)
        {
            Type thisType = GetType();
            foreach (var originalProp in original.GetType().GetProperties())
            {
                var thisProp = thisType.GetProperty(originalProp.Name);
                thisProp.SetValue(this, originalProp.GetValue(original, null), null);
            }
        }

        public static bool IsCopy(I original)
        {
            return original == null || original is DeepCopy<I>;
        }
    }

    public static class DeepCopyFactory
    {
        public static I CreateCopy<I>(I original)
            where I : class
        {
            Type type = typeof(I);
            if (type == typeof(IDataObject))
            {
                return DeepDataObject.CreateCopy(original as IDataObject) as I;
            }
            if (type == typeof(ITaskObject))
            {
                return DeepTaskObject.CreateCopy(original as ITaskObject) as I;
            }
            if (type == typeof(IPerson))
            {
                return DeepPerson.CreateCopy(original as IPerson) as I;
            }
            if (type == typeof(IType))
            {
                return DeepType.CreateCopy(original as IType) as I;
            }
            if (type == typeof(IOrganisationUnit))
            {
                return DeepOrganisationUnit.CreateCopy(original as IOrganisationUnit) as I;
            }
            if (type == typeof(IPosition))
            {
                return DeepPosition.CreateCopy(original as IPosition) as I;
            }
            if (type == typeof(IAttribute))
            {
                return DeepAttribute.CreateCopy(original as IAttribute) as I;
            }           
            if (type == typeof(IFile))
            {
                return DeepFile.CreateCopy(original as IFile) as I;
            }
            if (type == typeof(IFilesSnapshot))
            {
                return DeepFilesSnapshot.CreateCopy(original as IFilesSnapshot) as I;
            }
            if (type == typeof(ISignature))
            {
                return DeepSignature.CreateCopy(original as ISignature) as I;
            }
            if (type == typeof(IStageObject))
            {
                return DeepStageObject.CreateCopy(original as IStageObject) as I;
            }
            if (type == typeof(IWorkflowObject))
            {
                return DeepWorkflowObject.CreateCopy(original as IWorkflowObject) as I;
            }
            if (type == typeof(ILockInfo))
            {
                return DeepLockInfo.CreateCopy(original as ILockInfo) as I;
            }
            if (type == typeof(IAccess))
            {
                return DeepAccess.CreateCopy(original as IAccess) as I;
            }
            return original;
        }
    }
}
