using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayGenerationContextAttributeProvider : IGeneratorContextAttributeProvider
    {
        private readonly IReadOnlyTimeOfDayManager _readOnlyTimeOfDayManager;

        public TimeOfDayGenerationContextAttributeProvider(IReadOnlyTimeOfDayManager readOnlyTimeOfDayManager)
        {
            _readOnlyTimeOfDayManager = readOnlyTimeOfDayManager;
        }

        public IEnumerable<IGeneratorAttribute> GetAttributes()
        {
            yield return new GeneratorAttribute(
                new StringIdentifier("time-of-day"),
                new IdentifierGeneratorAttributeValue(_readOnlyTimeOfDayManager.TimeOfDay),
                false);
        }
    }
}