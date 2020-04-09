using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay.Api
{
    public interface IReadOnlyTimeOfDayManager
    {
        IIdentifier TimeOfDay { get; }

        double CyclePercent { get; }
    }
}