using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public interface ITimeOfDayConverter
    {
        IIdentifier GetTimeOfDay(double timeCyclePercent);
    }
}