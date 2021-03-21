using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public sealed class ElapsedTurnsTriggerMechanicSystem : IDiscoverableSystem
    {
        private readonly IElapsedTurnsTriggerSourceMechanic _elapsedTurnsTriggerSourceMechanic;

        public ElapsedTurnsTriggerMechanicSystem(IElapsedTurnsTriggerSourceMechanic elapsedTurnsTriggerSourceMechanic)
        {
            _elapsedTurnsTriggerSourceMechanic = elapsedTurnsTriggerSourceMechanic;
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            _elapsedTurnsTriggerSourceMechanic.Update(turnInfo);
        }
    }
}
