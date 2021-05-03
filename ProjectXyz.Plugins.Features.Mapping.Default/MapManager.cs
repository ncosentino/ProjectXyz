using System;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapManager : IMapManager
    {
        private readonly IMapIdentifiers _mapIdentifiers;
        private readonly IMapRepository _mapRepository;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IMapGameObjectRepository _mapGameObjectRepository;
        private readonly IPathFinderFactory _pathFinderFactory;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IGameObjectRepository _gameObjectRepository;

        public MapManager(
            IMapIdentifiers mapIdentifiers,
            IMapRepository mapRepository,
            IMapGameObjectManager mapGameObjectManager,
            IMapGameObjectRepository mapGameObjectRepository,
            IPathFinderFactory pathFinderFactory,
            IFilterContextFactory filterContextFactory,
            IGameObjectRepository gameObjectRepository)
        {
            _mapIdentifiers = mapIdentifiers;
            _mapRepository = mapRepository;
            _mapGameObjectManager = mapGameObjectManager;
            _mapGameObjectRepository = mapGameObjectRepository;
            _pathFinderFactory = pathFinderFactory;
            _filterContextFactory = filterContextFactory;
            _gameObjectRepository = gameObjectRepository;
        }

        public event EventHandler<EventArgs> MapChanging;

        public event EventHandler<EventArgs> MapChanged;

        public event EventHandler<EventArgs> MapPopulated;

        public IGameObject ActiveMap { get; private set; }

        public IPathFinder PathFinder { get; private set; }

        public void UnloadMap()
        {
            MapChanging?.Invoke(this, EventArgs.Empty);

            ActiveMap = null;
            PathFinder = null;

            MapChanged?.Invoke(this, EventArgs.Empty);

            _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects);
            _mapGameObjectManager.Synchronize();

            MapPopulated?.Invoke(this, EventArgs.Empty);
        }

        public void SwitchMap(IFilterContext filterContext)
        {
            MapChanging?.Invoke(this, EventArgs.Empty);

            var nextMap = _mapRepository.LoadMaps(filterContext).Single();

            if (ActiveMap != null)
            {
                // no op!
                if (ActiveMap == nextMap)
                {
                    return;
                }

                foreach (var gameObject in _mapGameObjectManager.GameObjects)
                {
                    _gameObjectRepository.Save(gameObject);
                }

                var mapGameObjectsToSave = _mapGameObjectManager
                    .GameObjects
                    .Where(x => !x.Has<ISkipMapSaveStateBehavior>());
                _mapGameObjectRepository.SaveState(
                    ActiveMap,
                    mapGameObjectsToSave);
            }

            ActiveMap = nextMap;
            PathFinder = _pathFinderFactory.CreateForMap(ActiveMap);

            MapChanged?.Invoke(this, EventArgs.Empty);

            var mapGameObjectsToRemove = _mapGameObjectManager
                .GameObjects
                .Where(x => !x.Has<IAlwaysLoadWithMapBehavior>());
            _mapGameObjectManager.MarkForRemoval(mapGameObjectsToRemove);

            var mapId = ActiveMap.GetOnly<IIdentifierBehavior>().Id;
            var mapGameObjectsToAdd = _mapGameObjectRepository
                .LoadForMap(mapId)
                .Concat(_gameObjectRepository
                    .LoadAll()
                    .Where(x => x.Has<IAlwaysLoadWithMapBehavior>()));
            _mapGameObjectManager.MarkForAddition(mapGameObjectsToAdd);
            _mapGameObjectManager.Synchronize();

            MapPopulated?.Invoke(this, EventArgs.Empty);
        }

        public void SwitchMap(IIdentifier mapId)
        {
            var filterContext = _filterContextFactory.CreateFilterContextForSingle(new[]
            {
                new FilterAttribute(
                    _mapIdentifiers.FilterContextMapIdentifier,
                    new IdentifierFilterAttributeValue(mapId),
                    true)
            });
            SwitchMap(filterContext);
        }
    }
}