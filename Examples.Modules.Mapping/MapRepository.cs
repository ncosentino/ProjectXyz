using System;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Examples.Modules.Mapping
{
    public sealed class MapRepository : IMapRepository
    {
        public IMap LoadMap(IIdentifier mapId)
        {
            throw new NotImplementedException();
        }
    }
}
