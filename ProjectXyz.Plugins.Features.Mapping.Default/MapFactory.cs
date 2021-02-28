using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapFactory : IMapFactory
    {
        private readonly IBehaviorManager _behaviorManager;

        public MapFactory(IBehaviorManager behaviorManager)
        {
            _behaviorManager = behaviorManager;
        }

        public IMap Create(
            IIdentifier id,
            IEnumerable<IMapLayer> layers,
            IEnumerable<IBehavior> behaviors)
        {
            var map = new Map(
                id,
                layers,
                behaviors);
            _behaviorManager.Register(map, map.Behaviors);
            return map;
        }
    }
}