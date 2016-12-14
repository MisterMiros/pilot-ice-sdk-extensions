﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepLockInfo : DeepCopy<ILockInfo>, ILockInfo
    {
        private DeepLockInfo(ILockInfo original) : base(original) { }

        public static ILockInfo CreateCopy(ILockInfo original)
        {
            return IsCopy(original) ? original : new DeepLockInfo(original);
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

    public static class ILockInfoDeepCopyExtension
    {
        public static ILockInfo Copy(this ILockInfo original)
        {
            return DeepLockInfo.CreateCopy(original);
        }
    }
}
