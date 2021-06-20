using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapManager : IMapManager
    {
        private readonly IMapIdentifiers _mapIdentifiers;
        private readonly IMapRepository _mapRepository;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IMapStateRepository _mapStateRepository;
        private readonly IMapGameObjectRepository _mapGameObjectRepository;
        private readonly IPathFinderFactory _pathFinderFactory;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly Lazy<IGameObjectRepository> _lazyGameObjectRepository;
        private readonly Lazy<IRosterManager> _lazyRosterManager;

        public MapManager(
            IMapIdentifiers mapIdentifiers,
            IMapRepository mapRepository,
            IMapGameObjectManager mapGameObjectManager,
            IMapStateRepository mapStateRepository,
            IMapGameObjectRepository mapGameObjectRepository,
            IPathFinderFactory pathFinderFactory,
            IFilterContextFactory filterContextFactory,
            Lazy<IGameObjectRepository> lazyGameObjectRepository,
            Lazy<IRosterManager> lazyRosterManager)
        {
            _mapIdentifiers = mapIdentifiers;
            _mapRepository = mapRepository;
            _mapGameObjectManager = mapGameObjectManager;
            _mapStateRepository = mapStateRepository;
            _mapGameObjectRepository = mapGameObjectRepository;
            _pathFinderFactory = pathFinderFactory;
            _filterContextFactory = filterContextFactory;
            _lazyGameObjectRepository = lazyGameObjectRepository;
            _lazyRosterManager = lazyRosterManager;
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

            _mapGameObjectManager.ClearGameObjectsImmediate();

            MapPopulated?.Invoke(this, EventArgs.Empty);
        }

        public async Task SaveActiveMapStateAsync()
        {
            foreach (var gameObject in _mapGameObjectManager.GameObjects)
            {
                _lazyGameObjectRepository.Value.Save(gameObject);
            }

            if (!ActiveMap.Has<IIgnoreSavingGameObjectStateBehavior>())
            {
                var mapGameObjectIdsToSave = _mapGameObjectManager
                    .GameObjects
                    .Where(x => !x.Has<ISkipMapSaveStateBehavior>())
                    .Select(x => x.GetOnly<IReadOnlyIdentifierBehavior>().Id);
                _mapStateRepository.SaveState(
                    ActiveMap.GetOnly<IReadOnlyIdentifierBehavior>().Id,
                    mapGameObjectIdsToSave);
            }
        }

        public async Task SwitchMapAsync(IFilterContext filterContext)
        {
            MapChanging?.Invoke(this, EventArgs.Empty);

            var nextMap = (await _mapRepository.LoadMapsAsync(filterContext)).Single();

            if (ActiveMap != null)
            {
                // no op!
                if (ActiveMap == nextMap)
                {
                    return;
                }

                await SaveActiveMapStateAsync();
            }

            ActiveMap = nextMap;
            PathFinder = _pathFinderFactory.CreateForMap(ActiveMap);

            MapChanged?.Invoke(this, EventArgs.Empty);

            var mapId = ActiveMap.GetOnly<IReadOnlyIdentifierBehavior>().Id;
            var mapGameObjectsToAdd = new HashSet<IGameObject>((await _mapGameObjectRepository
                .LoadForMapAsync(mapId))
                .Concat(_lazyGameObjectRepository
                    .Value
                    .Load(new[] { new PredicateFilter(x => x.Has<IAlwaysLoadWithMapBehavior>()) }))
                .Concat(_lazyRosterManager
                    .Value
                    .ActiveParty));
            var mapGameObjectsToRemove = _mapGameObjectManager
                .GameObjects
                .Where(x => !x.Has<IAlwaysLoadWithMapBehavior>())
                .Where(x => !mapGameObjectsToAdd.Contains(x));

            _mapGameObjectManager.MarkForRemoval(mapGameObjectsToRemove);
            _mapGameObjectManager.MarkForAddition(mapGameObjectsToAdd);
            _mapGameObjectManager.Synchronize();

            MapPopulated?.Invoke(this, EventArgs.Empty);
        }

        public async Task SwitchMapAsync(IIdentifier mapId)
        {
            var filterContext = _filterContextFactory.CreateFilterContextForSingle(new[]
            {
                new FilterAttribute(
                    _mapIdentifiers.FilterContextMapIdentifier,
                    new IdentifierFilterAttributeValue(mapId),
                    true)
            });
            await SwitchMapAsync(filterContext);
        }
    }
}