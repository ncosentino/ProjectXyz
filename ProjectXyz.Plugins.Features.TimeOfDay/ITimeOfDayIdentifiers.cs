using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public interface ITimeOfDayIdentifiers
    {
        IIdentifier TimeOfDayStateTypeId { get; }

        IIdentifier CyclePercentStateId { get; }

        IIdentifier TimeOfDayFilterContextId { get; }
    }
}