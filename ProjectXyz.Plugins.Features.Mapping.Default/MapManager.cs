using System;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapManager : IMapManager
    {
        private readonly IMapIdentifiers _mapIdentifiers;
        private readonly IMapRepository _mapRepository;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IMapStateRepository _mapStateRepository;
        private readonly IMapGameObjectPopulator _mapGameObjectPopulator;
        private readonly IPathFinderFactory _pathFinderFactory;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly ILogger _logger;
        private readonly Lazy<IGameObjectRepository> _lazyGameObjectRepository;

        public MapManager(
            IMapIdentifiers mapIdentifiers,
            IMapRepository mapRepository,
            IMapGameObjectManager mapGameObjectManager,
            IMapStateRepository mapStateRepository,
            IMapGameObjectPopulator mapGameObjectPopulator,
            IPathFinderFactory pathFinderFactory,
            IFilterContextFactory filterContextFactory,
            ILogger logger,
            Lazy<IGameObjectRepository> lazyGameObjectRepository)
        {
            _mapIdentifiers = mapIdentifiers;
            _mapRepository = mapRepository;
            _mapGameObjectManager = mapGameObjectManager;
            _mapStateRepository = mapStateRepository;
            _mapGameObjectPopulator = mapGameObjectPopulator;
            _pathFinderFactory = pathFinderFactory;
            _filterContextFactory = filterContextFactory;
            _logger = logger;
            _lazyGameObjectRepository = lazyGameObjectRepository;
        }

        public event EventHandler<EventArgs> MapChanging;

        public event EventHandler<EventArgs> MapChanged;

        public event EventHandler<EventArgs> MapPopulated;

        public IGameObject ActiveMap { get; private set; }

        public IPathFinder PathFinder { get; private set; }

        public async Task UnloadMapAsync()
        {
            await MapChanging
                .InvokeOrderedAsync(this, EventArgs.Empty)
                .ConfigureAwait(false);

            ActiveMap = null;
            PathFinder = null;

            await MapChanged
                .InvokeOrderedAsync(this, EventArgs.Empty)
                .ConfigureAwait(false);

            _mapGameObjectManager.ClearGameObjectsImmediateAsync();

            await MapPopulated
                .InvokeOrderedAsync(this, EventArgs.Empty)
                .ConfigureAwait(false);
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
            await MapChanging
                .InvokeOrderedAsync(this, EventArgs.Empty)
                .ConfigureAwait(false);

            var nextMap = (await _mapRepository
                .LoadMapsAsync(filterContext)
                .ConfigureAwait(false))
                .Single();
            var mapId = nextMap.GetOnly<IReadOnlyIdentifierBehavior>().Id;

            _logger.Debug(
                $"Switching map to '{mapId}'...");

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

            await MapChanged
                .InvokeOrderedAsync(this, EventArgs.Empty)
                .ConfigureAwait(false);
            await _mapGameObjectPopulator
                .PopulateMapGameObjectsAsync(ActiveMap)
                .ConfigureAwait(false);
            await MapPopulated
                .InvokeOrderedAsync(this, EventArgs.Empty)
                .ConfigureAwait(false);
            _logger.Debug(
                $"Switched map to '{mapId}'.");
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
            await SwitchMapAsync(filterContext).ConfigureAwait(false);
        }
    }
}