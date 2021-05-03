using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapGameObjectManagerSystem : IDiscoverableSystem
    {
        private readonly IMapGameObjectManager _mutableGameObjectManager;

        public MapGameObjectManagerSystem(IMapGameObjectManager mutableGameObjectManager)
        {
            _mutableGameObjectManager = mutableGameObjectManager;
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            _mutableGameObjectManager.Synchronize();
        }
    }
}
