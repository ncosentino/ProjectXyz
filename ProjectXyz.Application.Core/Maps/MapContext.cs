using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Time;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Application.Core.Maps
{
    public sealed class MapContext : IMapContext
    {
        #region Constructors
        private MapContext(IObservableCalendar calendar)
        {
            Contract.Requires<ArgumentNullException>(calendar != null);
            Calendar = calendar;
        }
        #endregion

        #region Properties
        public IObservableCalendar Calendar { get; private set; }
        #endregion

        #region Methods
        public static IMapContext Create(IObservableCalendar calendar)
        {
            Contract.Requires<ArgumentNullException>(calendar != null);
            Contract.Ensures(Contract.Result<IMapContext>() != null);
            return new MapContext(calendar);
        }
        #endregion
    }
}
