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
        private static readonly IInterval<double> LENGTH_OF_DAY = new Interval<double>(10000);

        private readonly ITimeOfDayManager _timeOfDayManager;
        private IInterval _currentCycleTime;

        public TimeOfDaySystem(ITimeOfDayManager timeOfDayManager)
        {
            _timeOfDayManager = timeOfDayManager;
            _currentCycleTime = new Interval<double>(0);
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _currentCycleTime = _currentCycleTime.Add(elapsed);

            // FIXME: isn't there something modulo can do here... brain...
            var limited = _currentCycleTime;
            while (((IInterval<double>)limited).Value > LENGTH_OF_DAY.Value)
            {
                limited = limited.Subtract(LENGTH_OF_DAY);
            }

            _currentCycleTime = limited;

            _timeOfDayManager.CyclePercent = _currentCycleTime.Divide(LENGTH_OF_DAY);

            // TODO: actually calculate the time of day
            _timeOfDayManager.TimeOfDay = TimesOfDay.Dusk;
        }
    }
}