using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayManager : ITimeOfDayManager
    {
        private readonly ITimeOfDayConverter _timeOfDayConverter;

        public TimeOfDayManager(ITimeOfDayConverter timeOfDayConverter)
        {
            _timeOfDayConverter = timeOfDayConverter;
        }

        public IIdentifier TimeOfDay => _timeOfDayConverter.GetTimeOfDay(CyclePercent);

        public double CyclePercent { get; set; }
    }
}