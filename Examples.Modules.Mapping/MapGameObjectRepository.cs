using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Examples.Modules.GameObjects
{
    public sealed class MapGameObjectRepository : IMapGameObjectRepository
    {
        public IEnumerable<IGameObject> LoadForMap(IIdentifier mapId)
        {
            yield break;
        }
    }
}
