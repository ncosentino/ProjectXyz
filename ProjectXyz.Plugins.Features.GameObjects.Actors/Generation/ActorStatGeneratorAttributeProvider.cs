using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatFilterAttributeProvider : IDiscoverableFilterContextAttributeProvider
    {
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;
        private readonly IActorIdentifiers _actorIdentifiers;

        public ActorStatFilterAttributeProvider(
            IMapGameObjectManager mapGameObjectManager,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory,
            IActorIdentifiers actorIdentifiers)
        {
            _mapGameObjectManager = mapGameObjectManager;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
            _actorIdentifiers = actorIdentifiers;
        }

        public IEnumerable<IFilterAttribute> GetAttributes()
        {
            yield return new FilterAttribute(
                _actorIdentifiers.FilterContextActorStatsIdentifier,
                new ActorStatFilterAttributeValueProvider(
                    _mapGameObjectManager,
                    _statCalculationService,
                    _statCalculationContextFactory),
                false);
        }
    }
}