using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapGameObjectManagerSystem : ISystem
    {
        private readonly IMapGameObjectManager _mutableGameObjectManager;
        private readonly IMapManager _mapManager;
        private readonly IMapGameObjectRepository _gameObjectRepository;

        public MapGameObjectManagerSystem(
            IMapGameObjectManager mutableGameObjectManager,
            IMapManager mapManager,
            IMapGameObjectRepository gameObjectRepository)
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
