using System;
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

        public void SwitchMap(string mapResourceId)
        {
            ActiveMap = _mapRepository.LoadMap(mapResourceId);
            MapChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}