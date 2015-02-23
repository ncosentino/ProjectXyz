using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Time
{
    public interface IObservableCalendar : ICalendar
    {
        #region Events
        event EventHandler<EventArgs> DateTimeChanged;
        #endregion
    }
}
