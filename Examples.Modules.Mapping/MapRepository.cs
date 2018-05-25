using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Mapping;

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
