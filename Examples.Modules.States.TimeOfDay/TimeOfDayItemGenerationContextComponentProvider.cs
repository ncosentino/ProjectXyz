using System.Collections.Generic;
using ProjectXyz.Api.Items;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDayItemGenerationContextComponentProvider : IItemGenerationContextComponentProvider
    {
        private readonly ITimeOfDaySystem _timeOfDaySystem;

        public TimeOfDayItemGenerationContextComponentProvider(ITimeOfDaySystem timeOfDaySystem)
        {
            _timeOfDaySystem = timeOfDaySystem;
        }

        public IEnumerable<IItemGenerationContextComponent> CreateComponents()
        {
            yield return new TimeOfDayItemGenerationContextComponent(_timeOfDaySystem.TimeOfDay);
        }
    }
}