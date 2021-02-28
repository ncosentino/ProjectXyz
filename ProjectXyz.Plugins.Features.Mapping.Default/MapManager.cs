using System;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapManager : IMapManager
    {
        private readonly IMapRepository _mapRepository;

        public MapManager(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public event EventHandler<EventArgs> MapChanged;

        public IMap ActiveMap { get; private set; }

        public void SwitchMap(IIdentifier mapId)
        {
            ActiveMap = _mapRepository.LoadMap(mapId);
            MapChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}