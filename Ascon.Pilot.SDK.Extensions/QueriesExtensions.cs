using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ascon.Pilot.SDK.Extensions.DeepCopies;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class QueriesExtensions
    {
        public static IEnumerable<IDataObject> GetChildrenByQuery
            (this IObjectsRepository repo, string query, IDataObject root = null)
        {
            var matches = Regex.Matches(query, "(>|/)([^>/]+)").Cast<Match>().ToArray();
            root = root ?? repo.GetRootObjectNew();
            return GetObjectsByMatches(repo, root, matches);
        }

        private static IEnumerable<IDataObject> GetObjectsByMatches
            (IObjectsRepository repo, IDataObject @object, Match[] query)
        {
            if (!query.Any())
            {
                yield return @object;
                yield break;
            }
            char kind = query[0].Groups[1].Value[0];
            string typename = query[0].Groups[2].Value;
            bool allChildren = typename == "*";

            IType type = !allChildren ? repo.GetType(typename) : null;
            if (type != null && (type.Id == -1 || !@object.Type.CanContain(type)))
            {
                yield break;
            }

            foreach (var pair in @object.TypesByChildren)
            {
                IDataObject child = null;
                if (allChildren || type.Id == pair.Value)
                {
                    child = Extensions.Repository.Get<IDataObject>(pair.Key);
                    foreach (var dataObject in GetObjectsByMatches(repo, child, query.Skip(1).ToArray()))
                    {
                        yield return dataObject;
                    }
                }
                if (kind == '/')
                {
                    IType childType = Extensions.Repository.GetType(pair.Value);
                    if (allChildren || childType.CanContain(type.Id))
                    {
                        child = child ?? Extensions.Repository.Get<IDataObject>(pair.Key);
                        foreach (var dataObject in GetObjectsByMatches(repo, child, query))
                        {
                            yield return dataObject;
                        }
                    }
                }
            }
        }

        public static IDataObject GetParentOfType
            (this IDataObject dataObject, string typename)
        {
            IType type = Extensions.Repository.GetType(typename);
            return GetAncestorOfType(dataObject, type);
        }

        public static IDataObject GetParentOfType
            (this IDataObject dataObject, int typeid)
        {
            IType type = Extensions.Repository.GetType(typeid);
            return GetAncestorOfType(dataObject, type);
        }

        public static IDataObject GetAncestorOfType(this IDataObject dataObject, IType type)
        {
            if (!type.CanContain(dataObject.Type))
            {
                return null;
            }
            return GetAncestorOfTypeRec(dataObject.GetParent(), type);
        }

        private static IDataObject GetAncestorOfTypeRec(IDataObject dataObject, IType type)
        {
            if (type.Id == dataObject.Type.Id) { return dataObject; }
            if (type.Id == 0)
            {
                return null;
            }
            return GetAncestorOfTypeRec(dataObject.GetParent(), type);
        }

        public static IEnumerable<ITaskObject> GetTasks(this IObjectsRepository repo)
        {
            IDataObject root = repo.GetTaskRootObject();
            return GetTasksRec(repo, root);
        }

        private static IEnumerable<ITaskObject> GetTasksRec(IObjectsRepository repo, IDataObject root)
        {
            if (root.Type.Name == SystemTypeNames.TASK)
            {
                yield return root.ToTask();
                yield break;
            }
            else
            {
                foreach (var child in root.GetChildren())
                {
                    foreach (var task in GetTasksRec(repo, child))
                    {
                        yield return task;
                    }
                }
            }
        }

        public static bool IsAncestor(this IDataObject dataObject, IDataObject descendant)
        {
            if (!dataObject.Type.CanContain(descendant.Type))
            {
                return false;
            }
            return IsAncestorRec(dataObject, descendant);
        }

        private static bool IsAncestorRec(IDataObject dataObject, IDataObject descendant)
        {
            if (descendant.ParentId == dataObject.Id)
            {
                return true;
            }
            if (descendant.ParentId == SystemObjectIds.RootObjectId)
            {
                return false;
            }
            return IsAncestorRec(dataObject, descendant.GetParent());
        }

        public static bool IsDescendant(this IDataObject dataObject, IDataObject ancestor)
        {
            return IsAncestor(ancestor, dataObject);
        }
    }
}
