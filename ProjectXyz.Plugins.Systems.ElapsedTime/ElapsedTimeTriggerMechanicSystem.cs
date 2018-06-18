using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Systems.ElapsedTime
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
                .GetFirst<Api.Framework.Entities.IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _elapsedTimeTriggerSourceMechanic.Update(elapsed);
        }
    }
}
