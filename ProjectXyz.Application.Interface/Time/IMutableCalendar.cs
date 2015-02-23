using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Time
{
    public interface IMutableCalendar : IObservableCalendar, IUpdateElapsedTime
    {
    }
}
