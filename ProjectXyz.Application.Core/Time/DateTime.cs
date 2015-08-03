using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Time;

namespace ProjectXyz.Application.Core.Time
{
    public sealed class DateTime : IDateTime
    {
        #region Constructors
        private DateTime(int year, int day, int hours, int minutes, int seconds, int milliseconds)
        {
            Year = year;
            Day = day;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }
        #endregion

        #region Properties
        public int Day { get; private set; }

        public int Year { get; private set; }

        public int Hours { get; private set; }

        public int Minutes { get; private set; }

        public int Seconds { get; private set; }

        public int Milliseconds { get; private set; }
        #endregion

        #region Methods
        public static IDateTime Create(int year, int day, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(year, day, hours, minutes, seconds, milliseconds);
        }

        public override string ToString()
        {
            return string.Format("Year {0}, Day {1}, {2:00}:{3:00}:{4:00}.{5:00}:", Year, Day, Hours, Minutes, Seconds, Milliseconds);
        }
        #endregion
    }
}
