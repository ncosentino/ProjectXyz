using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
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

        public async Task UpdateAsync(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            _elapsedTurnsTriggerSourceMechanic.Update(turnInfo);
        }
    }
}
