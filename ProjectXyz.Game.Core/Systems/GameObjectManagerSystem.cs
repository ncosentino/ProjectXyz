using System.Collections.Generic;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Systems;

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
