using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayItemGenerationContextComponent : ITimeOfDayItemGenerationContextComponent
    {
        public TimeOfDayItemGenerationContextComponent(IIdentifier timeOfDay)
        {
            TimeOfDay = timeOfDay;
        }

        public IIdentifier TimeOfDay { get; }
    }
}