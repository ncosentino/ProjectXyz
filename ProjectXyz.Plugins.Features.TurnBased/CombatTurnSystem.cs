using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    // FIXME: this doesn't belong in this domain
    public sealed class CombatTurnSystem : IDiscoverableSystem
    {
        private ITurnBasedManager _turnBasedManager;

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
           
            // FIXME: figure out the turn order;

            // FIXME: set the next actor to have a turn
            // _turnBasedManager.ApplicableGameObjects = 
        }
    }
}
