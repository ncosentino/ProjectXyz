using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace ProjectXyz.Plugins.Features.TimeOfDay.Default
{
    public sealed class TimeOfDayGenerationContextAttributeProvider : IDiscoverableFilterContextAttributeProvider
    {
        private readonly IReadOnlyTimeOfDayManager _readOnlyTimeOfDayManager;
        private readonly ITimeOfDayIdentifiers _timeOfDayIdentifiers;

        public TimeOfDayGenerationContextAttributeProvider(
            IReadOnlyTimeOfDayManager readOnlyTimeOfDayManager,
            ITimeOfDayIdentifiers timeOfDayIdentifiers)
        {
            _readOnlyTimeOfDayManager = readOnlyTimeOfDayManager;
            _timeOfDayIdentifiers = timeOfDayIdentifiers;
        }

        public IEnumerable<IFilterAttribute> GetAttributes()
        {
            yield return new FilterAttribute(
                _timeOfDayIdentifiers.TimeOfDayFilterContextId,
                new IdentifierFilterAttributeValue(_readOnlyTimeOfDayManager.TimeOfDay),
                false);
        }
    }
}