using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Game.Interface.GameObjects;

namespace ProjectXyz.Game.Core.Systems
{
    public sealed class GameObjectManagerSystem : ISystem
    {
        private readonly IMutableGameObjectManager _mutableGameObjectManager;

        public GameObjectManagerSystem(IMutableGameObjectManager mutableGameObjectManager)
        {
            _mutableGameObjectManager = mutableGameObjectManager;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            _mutableGameObjectManager.Synchronize();
        }
    }
}
