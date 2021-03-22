using ProjectXyz.Plugins.Features.TimeOfDay.Api;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public interface ITimeOfDayManager : IReadOnlyTimeOfDayManager
    {
        new double CyclePercent { get; set; }
    }
}