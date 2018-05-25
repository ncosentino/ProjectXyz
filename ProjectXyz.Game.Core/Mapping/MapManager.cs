using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Mapping;

namespace ProjectXyz.Game.Core.Mapping
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