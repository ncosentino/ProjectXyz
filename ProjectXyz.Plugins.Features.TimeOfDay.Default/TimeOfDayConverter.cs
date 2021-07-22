using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.TimeOfDay.Default;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayConverter : ITimeOfDayConverter
    {
        public IIdentifier GetTimeOfDay(double timeCyclePercent)
        {
            return new StringIdentifier("day");
        }
    }
}