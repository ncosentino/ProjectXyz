using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayGenerationContextAttributeProvider : IGeneratorContextAttributeProvider
    {
        private readonly ITimeOfDaySystem _timeOfDaySystem;

        public TimeOfDayGenerationContextAttributeProvider(ITimeOfDaySystem timeOfDaySystem)
        {
            _timeOfDaySystem = timeOfDaySystem;
        }

        public IEnumerable<IGeneratorAttribute> GetAttributes()
        {
            yield return new GeneratorAttribute(
                new StringIdentifier("time-of-day"),
                new IdentifierGeneratorAttributeValue(_timeOfDaySystem.TimeOfDay));
        }
    }
}