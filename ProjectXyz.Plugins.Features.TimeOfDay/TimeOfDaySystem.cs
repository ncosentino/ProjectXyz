using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDaySystem : ITimeOfDaySystem
    {
        private readonly ITimeOfDayManager _timeOfDayManager;
        private readonly ITimeOfDayConfiguration _timeOfDayConfiguration;
        private double _currentCycleTime;

        public TimeOfDaySystem(
            ITimeOfDayManager timeOfDayManager,
            ITimeOfDayConfiguration timeOfDayConfiguration)
        {
            _timeOfDayManager = timeOfDayManager;
            _timeOfDayConfiguration = timeOfDayConfiguration;
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsedTurns = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value
                .ElapsedTurns;
            _currentCycleTime += elapsedTurns;

            // FIXME: isn't there something modulo can do here... brain...
            var limited = _currentCycleTime;
            while (limited > _timeOfDayConfiguration.LengthOfDayInTurns)
            {
                limited -= _timeOfDayConfiguration.LengthOfDayInTurns;
            }

            _currentCycleTime = limited;
            _timeOfDayManager.CyclePercent = _currentCycleTime / _timeOfDayConfiguration.LengthOfDayInTurns;
        }
    }
}