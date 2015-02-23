using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Time;

namespace ProjectXyz.Application.Core.Time
{
    public class Calendar : IMutableCalendar
    {
        #region Constants
        private const int DAYS_PER_YEAR = 365;
        private const float SCALE = 100;
        #endregion

        #region Fields
        private TimeSpan _time;
        #endregion

        #region Constructors
        private Calendar(IDateTime dateTime)
        {
            this.DateTime = dateTime;
            
            _time = new TimeSpan(
                dateTime.Year * DAYS_PER_YEAR + dateTime.Day,
                dateTime.Hours,
                dateTime.Minutes,
                dateTime.Seconds,
                dateTime.Milliseconds);
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> DateTimeChanged;
        #endregion

        #region Properties
        public IDateTime DateTime
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IMutableCalendar Create(IDateTime dateTime)
        {
            return new Calendar(dateTime);
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            var oldTime = _time;
            _time += TimeSpan.FromTicks((long)(elapsedTime.Ticks * SCALE));

            if (oldTime == _time)
            {
                return;
            }

            int years = _time.Days / DAYS_PER_YEAR;
            
            this.DateTime = Time.DateTime.Create(
                years,
                _time.Days - years * DAYS_PER_YEAR,
                _time.Hours,
                _time.Minutes,
                _time.Seconds,
                _time.Milliseconds);

            var handler = DateTimeChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
