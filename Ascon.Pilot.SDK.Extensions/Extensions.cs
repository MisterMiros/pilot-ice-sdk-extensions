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
        static IObjectsRepository _repository = null;
        public static IObjectsRepository Repository
        {
            get
            {
                if (_repository == null)
                {
                    throw new InvalidOperationException("В static классе Extensions не задано свойство Repository");
                }
                return _repository;
            }
            set
            {
                _repository = value;
            }
        }

        public static int Timeout
        {
            get; set;
        }

        public static bool UseDeepCopies
        {
            get; set;
        }

        public static IAttributeFormatParser AttributeFormatParser
        {
            get; set;
        }

        public const string THREAD_NAME = "PilotSDKExtensions";

        static Extensions()
        {
            Timeout = 10000;
            UseDeepCopies = false;
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

        public static bool Start(ThreadStart action)
        {         
            if (Repository != null)
            {
                Thread thread = new Thread(action);
                thread.Name = THREAD_NAME;
                thread.Start();
                return true;
            }
            return false;
        }
    }
}
