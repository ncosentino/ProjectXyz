using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayManager : ITimeOfDayManager
    {
        public IIdentifier TimeOfDay { get; set; }

        public double CyclePercent { get; set; }
    }
}