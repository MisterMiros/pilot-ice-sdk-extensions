using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static void Initialize(IObjectsRepository repo, IErrorHandler errorHandler = null, int timeout = 10000)
        {
            Repository = repo;
            ErrorHandler = errorHandler ?? new BaseErrorHandler();
            Timeout = timeout;
            UseDeepCopies = false;
        }

        public static void Start(ThreadStart start)
        {
            Thread thread = new Thread(start);
            thread.Name = "ExtensionThread";
            thread.Start();
        }

        private class BaseErrorHandler : IErrorHandler
        {
            public void Handle(Exception ex)
            {
            }
        }
    }
}
