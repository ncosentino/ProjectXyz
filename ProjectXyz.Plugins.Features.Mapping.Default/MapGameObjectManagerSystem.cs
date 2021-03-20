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

        public MapGameObjectManagerSystem(IMapGameObjectManager mutableGameObjectManager)
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
