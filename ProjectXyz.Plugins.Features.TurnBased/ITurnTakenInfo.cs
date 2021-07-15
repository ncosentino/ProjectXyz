using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface ITurnTakenInfo
    {
        IGameObject Actor { get; }

        IFrozenCollection<IGameObject> ApplicableGameObjects { get; }
    }
}
