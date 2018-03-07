using System.Collections.Generic;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Systems;

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
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _elapsedTimeTriggerSourceMechanic.Update(elapsed);
        }
    }
}
