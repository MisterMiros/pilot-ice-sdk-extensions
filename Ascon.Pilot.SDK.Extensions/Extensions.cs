using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK.Extensions;

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

        public static void Initialize(IObjectsRepository repo, IErrorHandler errorHandler)
        {
            Repository = repo;
            ErrorHandler = errorHandler;
        }
    }
}
