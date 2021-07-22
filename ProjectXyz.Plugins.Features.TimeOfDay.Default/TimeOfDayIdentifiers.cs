using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay.Default
{
    public sealed class TimeOfDayIdentifiers : ITimeOfDayIdentifiers
    {
        public IIdentifier TimeOfDayStateTypeId { get; } = new StringIdentifier("Time of Day");

        public IIdentifier CyclePercentStateId { get; } = new StringIdentifier("Cycle Percent");

        public IIdentifier TimeOfDayFilterContextId { get; } = new StringIdentifier("time-of-day");
    }
}