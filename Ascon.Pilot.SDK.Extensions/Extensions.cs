using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

        public static void Initialize(IObjectsRepository repo, IErrorHandler errorHandler, int timeout = 10000)
        {
            Repository = repo;
            ErrorHandler = errorHandler;
            Timeout = timeout;
        }

        public static void Start(ThreadStart start)
        {
            Thread thread = new Thread(start);
            thread.Name = "ExtensionThread";
            thread.Start();
        }
    }
}
