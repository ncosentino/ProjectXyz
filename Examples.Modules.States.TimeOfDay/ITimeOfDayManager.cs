using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.TimeOfDay.Api;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public interface ITimeOfDayManager : IReadOnlyTimeOfDayManager
    {
        new IIdentifier TimeOfDay { get; set; }

        new double CyclePercent { get; set; }
    }
}