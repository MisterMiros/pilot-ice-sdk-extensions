using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.Objects
{
    public static class IObjectsRepositoryExtensions
    {
        public static IEnumerable<IObservable<I>> Subscribe<I>(this IObjectsRepository repo, IEnumerable<Guid> guids)
        {
            Guid[] activeGuid = new Guid[1];
            foreach (var guid in guids)
            {
                activeGuid[0] = guid;
                if (typeof(I) == typeof(IDataObject))
                {
                    yield return (IObservable<I>)repo.SubscribeObjects(activeGuid);
                }
                if (typeof(I) == typeof(ITaskObject))
                {
                    yield return (IObservable<I>)repo.SubscribeTasks(activeGuid);
                }
                if (typeof(I) == typeof(IWorkflowObject))
                {
                    yield return (IObservable<I>)repo.SubscribeWorkflow(activeGuid);
                }
                if (typeof(I) == typeof(IStageObject))
                {
                    yield return (IObservable<I>)repo.SubscribeStageObjects(activeGuid);
                }
                if (typeof(I) == typeof(ITaskMessage))
                {
                    yield return (IObservable<I>)repo.SubscribeTaskMessages(activeGuid);
                }
            }
        }
    }
}
