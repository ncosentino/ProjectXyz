using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Plugins.Features.TimeOfDay.Api;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayGenerationContextAttributeProvider : IDiscoverableFilterContextAttributeProvider
    {
        private readonly IReadOnlyTimeOfDayManager _readOnlyTimeOfDayManager;

        public TimeOfDayGenerationContextAttributeProvider(IReadOnlyTimeOfDayManager readOnlyTimeOfDayManager)
        {
            _readOnlyTimeOfDayManager = readOnlyTimeOfDayManager;
        }

        public IEnumerable<IFilterAttribute> GetAttributes()
        {
            yield return new FilterAttribute(
                new StringIdentifier("time-of-day"),
                new IdentifierFilterAttributeValue(_readOnlyTimeOfDayManager.TimeOfDay),
                false);
        }
    }
}