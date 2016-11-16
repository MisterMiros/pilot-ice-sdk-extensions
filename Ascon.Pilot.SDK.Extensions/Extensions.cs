using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static int Timeout
        {
            get; private set;
        }

        public static bool UseDeepCopies
        {
            get; set;
        }

        public static IAttributeFormatParser AttributeFormatParser
        {
            get; private set;
        }

        public static I CreateCopy<I>(I original)
            where I : class
        {
            if (UseDeepCopies)
            {
                return DeepCopyFactory.CreateCopy(original);
            }
            else
            {
                return original;
            }
        }

        private static bool _initialized = false;

        public static void Initialize(IObjectsRepository repo, IAttributeFormatParser parser = null, int timeout = 10000)
        {
            if (!_initialized)
            {
                Repository = repo;
                Timeout = timeout;
                UseDeepCopies = false;
                _initialized = true;
                AttributeFormatParser = parser;
            }
        }

        public static void Start(ThreadStart action)
        {         
            if (_initialized)
            {
                Thread thread = new Thread(action);
                thread.Name = "Extensions";
                thread.Start();
            }
        }
    }
}
