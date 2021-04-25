using System;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
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

        public MapManager(
            IMapIdentifiers mapIdentifiers,
            IMapRepository mapRepository,
            IMapGameObjectManager mapGameObjectManager,
            IMapGameObjectRepository mapGameObjectRepository,
            IPathFinderFactory pathFinderFactory,
            IFilterContextFactory filterContextFactory)
        {
            _mapIdentifiers = mapIdentifiers;
            _mapRepository = mapRepository;
            _mapGameObjectManager = mapGameObjectManager;
            _mapGameObjectRepository = mapGameObjectRepository;
            _pathFinderFactory = pathFinderFactory;
            _filterContextFactory = filterContextFactory;
        }

        public event EventHandler<EventArgs> MapChanging;

        public event EventHandler<EventArgs> MapChanged;

        public event EventHandler<EventArgs> MapPopulated;

        public IMap ActiveMap { get; private set; }

        public IPathFinder PathFinder { get; private set; }

        public void SwitchMap(IFilterContext filterContext)
        {
            MapChanging?.Invoke(this, EventArgs.Empty);
            ActiveMap = _mapRepository.LoadMaps(filterContext).Single();

            PathFinder = _pathFinderFactory.CreateForMap(ActiveMap);
            MapChanged?.Invoke(this, EventArgs.Empty);

            _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects);
            _mapGameObjectManager.MarkForAddition(_mapGameObjectRepository.LoadForMap(ActiveMap.Id));
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