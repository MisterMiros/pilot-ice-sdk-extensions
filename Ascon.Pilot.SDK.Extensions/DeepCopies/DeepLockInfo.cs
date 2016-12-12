using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepLockInfo : DeepCopy<ILockInfo>, ILockInfo
    {
        private DeepLockInfo(ILockInfo original) : base(original)
        {
            Date = original.Date;
            PersonId = original.PersonId;
            State = original.State;
        }

        public static ILockInfo CreateCopy(ILockInfo original)
        {
            if (original == null || original is DeepCopy<ILockInfo>)
            {
                return original;
            }
            return new DeepLockInfo(original);
        }

        public DateTime Date
        {
            get; private set;
        }

        public int PersonId
        {
            get; private set;
        }

        public LockState State
        {
            get; private set;
        }
    }
}
