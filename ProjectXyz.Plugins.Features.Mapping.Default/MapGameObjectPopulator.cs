using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapGameObjectPopulator : IMapGameObjectPopulator
    {
        private readonly ILogger _logger;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IMapGameObjectRepository _mapGameObjectRepository;
        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly Lazy<IGameObjectRepository> _lazyGameObjectRepository;
        private readonly Lazy<IReadOnlyCombatTurnManager> _lazyCombatTurnManager;

        public MapGameObjectPopulator(
            ILogger logger,
            IMapGameObjectManager mapGameObjectManager,
            IMapGameObjectRepository mapGameObjectRepository,
            Lazy<IRosterManager> lazyRosterManager,
            Lazy<IGameObjectRepository> lazyGameObjectRepository,
            Lazy<IReadOnlyCombatTurnManager> lazyCombatTurnManager)
        {
            _logger = logger;
            _mapGameObjectManager = mapGameObjectManager;
            _mapGameObjectRepository = mapGameObjectRepository;
            _lazyRosterManager = lazyRosterManager;
            _lazyGameObjectRepository = lazyGameObjectRepository;
            _lazyCombatTurnManager = lazyCombatTurnManager;
        }

        public async Task PopulateMapGameObjectsAsync(IIdentifier mapId)
        {
            _logger.Debug(
                $"Populating game objects for map ID '{mapId}'...");

            var mapGameObjectsToAdd = (await _mapGameObjectRepository
                .LoadForMapAsync(mapId))
                .Concat(_lazyGameObjectRepository
                    .Value
                    .Load(new[] { new PredicateFilter(x => x.Has<IAlwaysLoadWithMapBehavior>()) }));

            var combatTurnManager = _lazyCombatTurnManager.Value;
            var rosterManager = _lazyRosterManager.Value;
            mapGameObjectsToAdd = combatTurnManager.InCombat
                ? mapGameObjectsToAdd.Concat(rosterManager.ActiveParty)
                : mapGameObjectsToAdd.AppendSingle(rosterManager.ActivePartyLeader);
            var mapGameObjectsToAddLookup = new HashSet<IGameObject>(mapGameObjectsToAdd);

            _logger.Debug(
                "Game objects to add to map:\r\n" +
                string.Join("\r\n,", mapGameObjectsToAddLookup
                    .Select(obj => obj.GetOnly<IReadOnlyIdentifierBehavior>().Id)
                    .Select(id => "\t" + id)));

            var mapGameObjectsToRemove = _mapGameObjectManager
                .GameObjects
                .Where(x => !x.Has<IAlwaysLoadWithMapBehavior>())
                .Where(x => !mapGameObjectsToAddLookup.Contains(x));
            _logger.Debug(
                "Game objects to remove from map:\r\n" +
                string.Join("\r\n,", mapGameObjectsToRemove
                    .Select(obj => obj.GetOnly<IReadOnlyIdentifierBehavior>().Id)
                    .Select(id => "\t" + id)));

            _mapGameObjectManager.MarkForRemoval(mapGameObjectsToRemove);
            _mapGameObjectManager.MarkForAddition(mapGameObjectsToAddLookup);
            _mapGameObjectManager.Synchronize();

            _logger.Debug(
                $"Populated game objects for map ID '{mapId}'.");
        }
    }
}