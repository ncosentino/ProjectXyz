using ProjectXyz.Plugins.Features.TimeOfDay.Default;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayConfiguration : ITimeOfDayConfiguration
    {
        public double LengthOfDayInTurns => 10;
    }
}