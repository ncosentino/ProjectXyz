using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Time;

namespace ProjectXyz.Application.Interface.Maps
{
    public interface IMapContext
    {
        #region Properties
        IObservableCalendar Calendar { get; }
        #endregion
    }
}
