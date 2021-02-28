using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Game.Core.Systems
{
    public sealed class GameObjectManagerSystem : ISystem
    {
        private readonly IMutableGameObjectManager _mutableGameObjectManager;
        private readonly IMapManager _mapManager;
        private readonly IGameObjectRepository _gameObjectRepository;

        public GameObjectManagerSystem(
            IMutableGameObjectManager mutableGameObjectManager,
            IMapManager mapManager,
            IGameObjectRepository gameObjectRepository)
        {
            _mutableGameObjectManager = mutableGameObjectManager;
            _mapManager = mapManager;
            _gameObjectRepository = gameObjectRepository;

            _mapManager.MapChanged += MapManager_MapChanged;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            _mutableGameObjectManager.Synchronize();
        }

        private void MapManager_MapChanged(object sender, EventArgs e)
        {
            _mutableGameObjectManager.MarkForRemoval(_mutableGameObjectManager.GameObjects);
            _mutableGameObjectManager.MarkForAddition(_gameObjectRepository.LoadForMap(_mapManager.ActiveMap.Id));
        }
    }
}
