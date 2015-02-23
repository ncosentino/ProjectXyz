using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Time
{
    public interface IDate
    {
        #region Properties
        int Day { get; }

        int Year { get; }
        #endregion
    }
}
