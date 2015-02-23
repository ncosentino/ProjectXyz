using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Time
{
    public interface ICalendar
    {
        #region Properties
        IDateTime DateTime { get; }
        #endregion
    }
}
