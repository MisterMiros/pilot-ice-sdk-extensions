using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepSignature : DeepCopy<ISignature>, ISignature
    {
        private DeepSignature(ISignature original) : base(original) { }

        [DeepCopyCreator(typeof(ISignature))]
        public static ISignature CreateCopy(ISignature original)
        {
            return IsCopy(original) ? original : new DeepSignature(original);
        }

        public Guid DatabaseId
        {
            get; private set;
        }

        public Guid Id
        {
            get; private set;
        }

        public int PositionId
        {
            get; private set;
        }

        string _requestedSigner;
        public string RequestedSigner
        {
            get
            {
                return _requestedSigner;
            }
            private set
            {
                _requestedSigner = CopyString(value);
            }
        }

        string _role;
        public string Role
        {
            get
            {
                return _role;
            }
            private set
            {
                _role = CopyString(value);
            }
        }

        string _sign;
        public string Sign
        {
            get
            {
                return _sign;
            }
            private set
            {
                _sign = CopyString(value);
            }
        }
    }

    public static class ISignatureDeepCopyExtension
    {
        public static ISignature Copy(this ISignature original)
        {
            return DeepSignature.CreateCopy(original);
        }
    }
}
