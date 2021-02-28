using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapRepository
    {
        IMap LoadMap(IIdentifier mapId);
    }
}