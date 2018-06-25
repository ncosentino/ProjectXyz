using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.ElapsedTime.Api;

namespace ProjectXyz.Plugins.Features.ElapsedTime
{
    public sealed class ElapsedTimeTriggerMechanicSystem : ISystem
    {
        private readonly IElapsedTimeTriggerSourceMechanic _elapsedTimeTriggerSourceMechanic;

        public ElapsedTimeTriggerMechanicSystem(IElapsedTimeTriggerSourceMechanic elapsedTimeTriggerSourceMechanic)
        {
            _elapsedTimeTriggerSourceMechanic = elapsedTimeTriggerSourceMechanic;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _elapsedTimeTriggerSourceMechanic.Update(elapsed);
        }
    }
}
