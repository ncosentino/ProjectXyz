using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay.Default
{
    public interface ITimeOfDayConverter
    {
        IIdentifier GetTimeOfDay(double timeCyclePercent);
    }
}