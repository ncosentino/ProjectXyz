using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public sealed class TurnInfo : ITurnInfo
    {
        public TurnInfo(
            IReadOnlyCollection<IGameObject> applicableGameObjects,
            IReadOnlyCollection<IGameObject> allGameObjects,
            double elapsedTurns,
            bool globalSync)
        {
            // NOTE: directly assign for perf reasons here. caller must provide
            // immutable collection
            ApplicableGameObjects = applicableGameObjects;
            AllGameObjects = allGameObjects;

            ElapsedTurns = elapsedTurns;
            GlobalSync = globalSync;
        }

        public IReadOnlyCollection<IGameObject> AllGameObjects { get; }

        public IReadOnlyCollection<IGameObject> ApplicableGameObjects { get; }

        public double ElapsedTurns { get; }

        public bool GlobalSync { get; }
    }
}
