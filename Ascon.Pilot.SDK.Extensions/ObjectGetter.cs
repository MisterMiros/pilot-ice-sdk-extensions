﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;
using System.Threading;

namespace Ascon.Pilot.SDK.Extensions
{
    public class ObjectGetter<I> where I : class
    {
        public delegate I DeepCopyCreator(I original);
        public delegate bool ObjectFilter(I obj);

        public DeepCopyCreator Creator { get; set; }
        public int TimeoutTime { get; set; }

        public ObjectGetter(ObjectFilter filter = null, DeepCopyCreator creator = null)
        {
            Creator = creator;
        }

        public static I GetObject(IObservable<I> observable)
        {
            var observer = new ObjectObserver();
            Thread thread = new Thread(observer.Observe);
            thread.Name = "ObjectGetter";
            thread.IsBackground = true;
            thread.Start(observable);

            observer._resetEvent.WaitOne(Extensions.Timeout);
            if (!observer._gotObject)
                throw new TimeoutException("Получение объекта данных из Pilot заняло более 10 секунд");

            while (observer._unsub == null) { }
            observer._unsub.Dispose();

            thread.Abort();

            return observer._obj;
        }

        public IEnumerable<I> GetObjects(IEnumerable<Guid> guids, IObjectSearcher<I> searcher = null)
        {
            if (searcher == null)
            {
                searcher = BaseSearcher.GetInstance();
            }
            return Extensions.Repository.SubscribeMany<I>(guids).SelectMany((obl) =>
            {
                I @object = GetObject(obl);
                if (Creator != null)
                {
                    @object = Creator(@object);
                }
                return searcher.SearchNext(@object, this);
            });
        }

        private class ObjectObserver : IObserver<I>
        {
            public bool _gotObject = false;
            public ManualResetEvent _resetEvent = new ManualResetEvent(false);
            public I _obj = default(I);
            public IDisposable _unsub;

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

            public void Observe(object observable)
            {
                _unsub = ((IObservable<I>)observable).Subscribe(this);
            }
        }

        private class BaseSearcher : IObjectSearcher<I>
        {
            static readonly BaseSearcher _instance = new BaseSearcher();
            public static BaseSearcher GetInstance()

            {
                return _instance;
            }

            private BaseSearcher() { }

            public IEnumerable<I> SearchNext(I @object, ObjectGetter<I> getter)
            {
                yield return @object;
            }
        }
    }

    public interface IObjectSearcher<I> where I : class
    {
        IEnumerable<I> SearchNext(I @object, ObjectGetter<I> getter);
    }
}
