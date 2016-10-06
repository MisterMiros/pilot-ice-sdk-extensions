using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.Pilot.SDK.Extensions
{
    public interface IErrorHandler
    {
        void Handle(Exception ex);
    }
}
