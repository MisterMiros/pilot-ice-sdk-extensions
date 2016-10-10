using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ascon.Pilot.SDK.Extensions.DeepCopies;

namespace Ascon.Pilot.SDK.Extensions.Queries
{
    public static class QueriesExtensions
    {
        public static IEnumerable<IDataObject> GetChildrenByQuery
            (this IObjectsRepository repo, string query, IDataObject root = null)
        {
            var matches = Regex.Matches(query, "(>|/)([^>/]+)").Cast<Match>().ToArray();
            root = root ?? repo.Get<IDataObject>(SystemObjectIds.RootObjectId);
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
            return GetParentOfType(dataObject, type);
        }

        public static IDataObject GetParentOfType
            (this IDataObject dataObject, int typeid)
        {
            IType type = Extensions.Repository.GetType(typeid);
            return GetParentOfType(dataObject, type);
        }

        public static IDataObject GetParentOfType
            (this IDataObject dataObject, IType type)
        {
            if (!type.CanContain(dataObject.Type))
            {
                return null;
            }
            IDataObject directParent = Extensions.Repository.Get<IDataObject>(dataObject.ParentId);
            return GetParentOfTypeRec(directParent, type);
        }

        private static IDataObject GetParentOfTypeRec
            (IDataObject dataObject, IType type)
        {
            if (type.Id == dataObject.Type.Id) { return dataObject; }
            if (type.Id == 0)
            {
                return null;
            }
            IDataObject directParent = Extensions.Repository.Get<IDataObject>(dataObject.ParentId);
            return GetParentOfTypeRec(directParent, type);
        }

    }
}
