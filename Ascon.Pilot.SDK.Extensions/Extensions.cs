using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK.Extensions;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class Extensions
    {
        private static IObjectsRepository _repo;

        public static IObjectsRepository Repository
        {
            get
            {
                return _repo;
            }
        }

        public static void Initialize(IObjectsRepository repo)
        {
            _repo = repo;
        }
    }
}
