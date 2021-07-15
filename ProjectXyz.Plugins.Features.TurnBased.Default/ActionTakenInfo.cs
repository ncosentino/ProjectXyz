
using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class ActionTakenInfo : IActionTakenInfo
    {
        public ActionTakenInfo(
            IGameObject actor,
            IFrozenCollection<IGameObject> applicableGameObjects)
        {
            Actor = actor;
            ApplicableGameObjects = applicableGameObjects;
        }

        public IGameObject Actor { get; }

        public IFrozenCollection<IGameObject> ApplicableGameObjects { get; }
    }
}
