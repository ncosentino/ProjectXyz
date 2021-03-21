using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased.Api
{
    public interface ITurnBasedManager : IReadOnlyTurnBasedManager
    {
        new bool SyncTurnsFromElapsedTime { get; set; }

        new bool ClearApplicableOnUpdate { get; set; }

        new bool GlobalSync { get; set; }

        void SetApplicableObjects(IEnumerable<IGameObject> gameObjects);
    }
}
