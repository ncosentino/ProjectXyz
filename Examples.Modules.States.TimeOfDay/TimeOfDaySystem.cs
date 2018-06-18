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
        private IInterval _current;

        public TimeOfDaySystem()
        {
            _current = new Interval<double>(0);
        }

        public IIdentifier TimeOfDay { get; private set; }

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
            TimeOfDay = TimesOfDay.Day;
        }
    }
}