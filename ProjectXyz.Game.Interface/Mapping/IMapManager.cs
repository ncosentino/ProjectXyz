using ProjectXyz.Api.Framework;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMapManager : IMapProvider
    {
        void SwitchMap(IIdentifier mapId);
    }
}