using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased.Api
{
    public interface ITurnInfo
    {
        IReadOnlyCollection<IGameObject> AllGameObjects { get; }

        IReadOnlyCollection<IGameObject> ApplicableGameObjects { get; }

        double ElapsedTurns { get; }

        bool GlobalSync { get; }
    }
}
