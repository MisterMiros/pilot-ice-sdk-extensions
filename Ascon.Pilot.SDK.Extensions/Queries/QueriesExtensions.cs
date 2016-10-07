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
        private class QuerySearcher : IObjectSearcher<IDataObject>
        {
            private IList<Match> _query;

            public delegate bool Filter(IDataObject dataObject);
            private Filter _filter;
            
            public QuerySearcher(string query, Filter filter = null)
            {
                _query = Regex.Matches(query, "(>|/)([^>/]+)").Cast<Match>().ToArray();
                _filter = filter ?? ((dObj) => true);
            }

            private QuerySearcher(IEnumerable<Match> query, Filter filter = null)
            {
                _query = query.ToArray();
                _filter = filter ?? ((dObj) => true);
            }

            public IEnumerable<IDataObject> SearchNext(IDataObject @object, ObjectGetter<IDataObject> getter)
            {
                if (!_query.Any())
                {
                    yield return @object;
                    yield break;
                }
                char kind = _query[0].Groups[1].Value[0];
                string typename = _query[0].Groups[2].Value;
                bool allChildren = typename == "*";
                IType type = !allChildren ? Extensions.Repository.GetType(typename) : null;
                if (!@object.Type.CanContain(type))
                {
                    yield break;
                }
                foreach (var pair in @object.TypesByChildren)
                {
                    IDataObject child = null;
                    if (allChildren || type.Id == pair.Value)
                    {
                        child = Extensions.Repository.Get<IDataObject>(pair.Key);
                        var searcher1 = new QuerySearcher(_query.Skip(1));
                        foreach (var dataObject in searcher1.SearchNext(child, getter))
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
                            var searcher = new QuerySearcher(_query);
                            foreach (var dataObject in searcher.SearchNext(child, getter))
                            {
                                yield return dataObject;
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<IDataObject> GetObjectsByQuery
            (this IObjectsRepository repo, string query)
        {
            var searcher = new QuerySearcher(query);
            var getter = new ObjectGetter<IDataObject>();
            return getter.GetObjects(SystemObjectIds.RootObjectId.ToArray(), searcher);
        }
    }
}
