using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public sealed class TimeOfDaySystem : ITimeOfDaySystem
    {
        private readonly ITimeOfDayManager _timeOfDayManager;
        private IInterval _current;

        public TimeOfDaySystem(ITimeOfDayManager timeOfDayManager)
        {
            _timeOfDayManager = timeOfDayManager;
            _current = new Interval<double>(0);
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _current = _current.Add(elapsed);

            // TODO: actually calculate the time of day
            _timeOfDayManager.TimeOfDay = TimesOfDay.Day;
        }
    }
}