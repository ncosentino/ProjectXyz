using ProjectXyz.Api.Framework;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMapRepository
    {
        IMap LoadMap(IIdentifier mapId);
    }
}