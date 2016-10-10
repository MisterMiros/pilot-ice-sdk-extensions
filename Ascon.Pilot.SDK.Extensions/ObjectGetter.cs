using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;
using System.Threading;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ObjectGetter
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
    }

    internal class ObjectObserver<I> : IObserver<I>
        where I : class
    {
        public bool _gotObject;
        public ManualResetEvent _resetEvent = new ManualResetEvent(false);
        public I _obj;
        public IDisposable _unsub;

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
            _resetEvent.Reset();
            _unsub = null;
            _gotObject = false;

            Thread thread = new Thread(() =>
            {
                _unsub = observable.Subscribe(this);
            });
            thread.Name = "ObjectGetter";
            thread.IsBackground = true;
            thread.Start();

            _resetEvent.WaitOne(Extensions.Timeout);
            if (!_gotObject)
            {
                throw new TimeoutException("Получение объекта данных из Pilot заняло более 10 секунд");
            }

            while (_unsub == null) { }
            _unsub.Dispose();
            thread.Abort();

            return Extensions.CreateCopy(_obj);
        }
    }
}
