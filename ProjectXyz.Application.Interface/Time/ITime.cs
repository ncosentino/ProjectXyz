using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Time
{
    public interface ITime
    {
        #region Properties
        int Hours { get; }

        int Minutes { get; }

        int Seconds { get; }

        int Milliseconds { get; }
        #endregion
    }
}
