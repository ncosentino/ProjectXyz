using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IActionInfo
    {
        IGameObject Actor { get; }

        IFrozenCollection<IGameObject> ApplicableGameObjects { get; }

        IFrozenHashSet<IGameObject> AllGameObjects { get; }

        double ElapsedActions { get; }
    }
}
