using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public interface IReadOnlyTimeOfDayManager
    {
        IIdentifier TimeOfDay { get; }

        double CyclePercent { get; }
    }
}