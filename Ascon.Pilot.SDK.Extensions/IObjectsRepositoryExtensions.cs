using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IObjectsRepositoryExtensions
    {
        public static IEnumerable<IObservable<I>> SubscribeMany<I>(this IObjectsRepository repo, IEnumerable<Guid> guids)
        {
            Guid[] activeGuid = new Guid[1];
            foreach (var guid in guids)
            {
                activeGuid[0] = guid;
                yield return SubscribeByType<I>(repo, activeGuid);
            }
        }

        public static IObservable<I> Subscribe<I>(this IObjectsRepository repo, Guid guid)
        {
            Guid[] guidArr = new Guid[] { guid };
            return SubscribeByType<I>(repo, guidArr);
        }

        private static IObservable<I> SubscribeByType<I>(IObjectsRepository repo, IEnumerable<Guid> guids)
        {
            if (typeof(I) == typeof(IDataObject))
            {
                return (IObservable<I>)repo.SubscribeObjects(guids);
            }
            if (typeof(I) == typeof(ITaskObject))
            {
                return (IObservable<I>)repo.SubscribeTasks(guids);
            }
            if (typeof(I) == typeof(IWorkflowObject))
            {
                return (IObservable<I>)repo.SubscribeWorkflow(guids);
            }
            if (typeof(I) == typeof(IStageObject))
            {
                return (IObservable<I>)repo.SubscribeStageObjects(guids);
            }
            if (typeof(I) == typeof(ITaskMessage))
            {
                return (IObservable<I>)repo.SubscribeTaskMessages(guids);
            }
            return null;
        }

        public static IEnumerable<I> GetMany<I>(this IObjectsRepository repo, IEnumerable<Guid> guids)
            where I : class
        {
            ObjectGetter<I> getter = new ObjectGetter<I>();
            return getter.GetObjects(guids);
        }

        public static I Get<I>(this IObjectsRepository repo, Guid guid)
            where I : class
        {
            return ObjectGetter<I>.GetObject(repo.Subscribe<I>(guid));
        }
    }
}
