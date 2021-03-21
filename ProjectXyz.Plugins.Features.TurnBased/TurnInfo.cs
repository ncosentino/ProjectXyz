using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public sealed class TurnInfo : ITurnInfo
    {
        public TurnInfo(
            IReadOnlyCollection<IGameObject> applicableGameObjects,
            double elapsedTurns,
            bool globalSync)
        {
            // NOTE: directly assign for perf reasons here
            ApplicableGameObjects = applicableGameObjects;
            ElapsedTurns = elapsedTurns;
        }

        public IReadOnlyCollection<IGameObject> ApplicableGameObjects { get; }

        public double ElapsedTurns { get; }

        public bool GlobalSync { get; }
    }
}
