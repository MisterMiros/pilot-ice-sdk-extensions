using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK.Extensions.DeepCopies;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class Extensions
    {
        public static IObjectsRepository Repository
        {
            get; private set;
        }

        public static IErrorHandler ErrorHandler
        {
            get; private set;
        }

        public static int Timeout
        {
            get; private set;
        }

        public static bool UseDeepCopies
        {
            get; set;
        }

        public static I CreateCopy<I>(I original)
            where I : class
        {
            if (UseDeepCopies)
            {
                return DeepCopy.CreateCopy(original);
            }
            else
            {
                return original;
            }
        }

        private static bool _initialized = false;

        public static void Initialize(IObjectsRepository repo, IErrorHandler errorHandler = null, int timeout = 10000)
        {
            if (!_initialized)
            {
                Repository = repo;
                ErrorHandler = errorHandler ?? new BaseErrorHandler();
                Timeout = timeout;
                UseDeepCopies = false;
                _initialized = true;
            }
            else
            {
                throw new InvalidOperationException("Расширения уже были инициализированы.");
            }
        }

        public static void Start(Action action)
        {         
            if (_initialized)
            {
                Task task = new Task(action);
                task.Start();
            }
            else
            {
                throw new InvalidOperationException("Расширения не инициализированы.");
            }
        }

        private class BaseErrorHandler : IErrorHandler
        {
            public void Handle(Exception ex)
            {
            }
        }
    }
}
