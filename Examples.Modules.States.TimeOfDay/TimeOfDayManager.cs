using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayManager : ITimeOfDayManager
    {
        public TimeOfDayManager()
        {
            // FIXME: hardcoding a ToD here seems wrong, but allowing a 
            // null ToD ID also seems wrong?
            TimeOfDay = TimesOfDay.Dawn;
        }

        public IIdentifier TimeOfDay { get; set; }

        public double CyclePercent { get; set; }
    }
}