using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface ITurnInfoFactory
    {
        ITurnInfo Create(
            IGameObject actor,
            IFrozenCollection<IGameObject> applicableGameObjects,
            double elapsedTurns);
    }
}
