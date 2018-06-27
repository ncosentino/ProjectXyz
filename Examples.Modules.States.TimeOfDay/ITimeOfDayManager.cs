using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public interface ITimeOfDayManager : IReadOnlyTimeOfDayManager
    {
        new IIdentifier TimeOfDay { get; set; }

        new double CyclePercent { get; set; }
    }
}