using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using ProjectXyz.Api.Items;
using ProjectXyz.Api.Items.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayItemGenerationContextComponentProvider : IItemGeneratorContextAttributeProvider
    {
        private readonly ITimeOfDaySystem _timeOfDaySystem;

        public TimeOfDayItemGenerationContextComponentProvider(ITimeOfDaySystem timeOfDaySystem)
        {
            _timeOfDaySystem = timeOfDaySystem;
        }

        public IEnumerable<IItemGeneratorAttribute> GetAttributes()
        {
            yield return new ItemGeneratorAttribute(
                new StringIdentifier("time-of-day"),
                new IdentifierItemGeneratorAttributeValue(_timeOfDaySystem.TimeOfDay));
        }
    }
}