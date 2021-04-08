using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapManager : IMapManager
    {
        private readonly IMapRepository _mapRepository;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IMapGameObjectRepository _mapGameObjectRepository;
        private readonly IPathFinderFactory _pathFinderFactory;

        public MapManager(
            IMapRepository mapRepository,
            IMapGameObjectManager mapGameObjectManager,
            IMapGameObjectRepository mapGameObjectRepository,
            IPathFinderFactory pathFinderFactory)
        {
            _mapRepository = mapRepository;
            _mapGameObjectManager = mapGameObjectManager;
            _mapGameObjectRepository = mapGameObjectRepository;
            _pathFinderFactory = pathFinderFactory;
        }

        public event EventHandler<EventArgs> MapChanging;

        public event EventHandler<EventArgs> MapChanged;

        public event EventHandler<EventArgs> MapPopulated;

        public IMap ActiveMap { get; private set; }

        public IPathFinder PathFinder { get; private set; }

        public void SwitchMap(IIdentifier mapId)
        {
            MapChanging?.Invoke(this, EventArgs.Empty);
            ActiveMap = _mapRepository.LoadMap(mapId);
            PathFinder = _pathFinderFactory.CreateForMap(ActiveMap);
            MapChanged?.Invoke(this, EventArgs.Empty);

            _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects);
            _mapGameObjectManager.MarkForAddition(_mapGameObjectRepository.LoadForMap(mapId));
            _mapGameObjectManager.Synchronize();
            MapPopulated?.Invoke(this, EventArgs.Empty);
        }
    }
}