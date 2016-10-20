using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;
using System.Threading;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ObjectGettingExtensions
    {
        public static IEnumerable<I> Get<I>(this IObjectsRepository repo, IEnumerable<Guid> guids)
            where I : class
        {
            var observer = ObjectObserver<I>.Instance;
            foreach (var observable in repo.SubscribeMany<I>(guids))
            {
                I obj;
                try { obj = observer.Observe(observable); }
                catch (Exception ex)
                {
                    Extensions.ErrorHandler.Handle(ex);
                    continue;
                }
                yield return obj;
            }
            yield break;
        }

        public static I Get<I>(this IObjectsRepository repo, Guid guid)
            where I : class
        {
            return ObjectObserver<I>.Instance.Observe(repo.Subscribe<I>(guid));
        }

        public static IDataObject GetRootObjectNew(this IObjectsRepository repo)
        {
            return repo.Get<IDataObject>(SystemObjectIds.RootObjectId);
        }

        public static IDataObject GetTaskRootObject(this IObjectsRepository repo)
        {
            return repo.Get<IDataObject>(SystemObjectIds.TaskRootObjectId);
        }
    }

    internal class ObjectObserver<I> : IObserver<I>, IDisposable
        where I : class
    {
        private bool _gotObject;
        private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        public I _obj;

        private ObjectObserver() { }

        static ObjectObserver<I> _instance = new ObjectObserver<I>();
        public static ObjectObserver<I> Instance
        {
            get
            {
                return _instance;
            }
        }

        public void OnNext(I obj)
        {
            if (!_gotObject)
            {
                _obj = obj;
                _gotObject = true;
                _resetEvent.Set();
            }
        }

        public void OnCompleted() { }
        public void OnError(Exception ex) { }

        public I Observe(IObservable<I> observable)
        {
            lock (this)
            {
                _resetEvent.Reset();
                IDisposable unsub = null;
                _gotObject = false;

                unsub = observable.Subscribe(this);
                _resetEvent.WaitOne(Extensions.Timeout);
                if (!_gotObject)
                {
                    throw new TimeoutException("Получение объекта данных из Pilot заняло более 10 секунд");
                }
                while (unsub == null) { }
                unsub.Dispose();

                return Extensions.CreateCopy(_obj);
            }
        }

        public void Dispose()
        {
            _resetEvent.Dispose();
        }

    }
}
