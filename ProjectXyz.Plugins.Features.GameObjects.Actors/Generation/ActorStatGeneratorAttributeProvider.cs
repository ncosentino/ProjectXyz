using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatFilterAttributeProvider : IDiscoverableFilterContextAttributeProvider
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;

        public ActorStatFilterAttributeProvider(
            IGameObjectManager gameObjectManager,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory)
        {
            _gameObjectManager = gameObjectManager;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
        }

        public IEnumerable<IFilterAttribute> GetAttributes()
        {
            yield return new FilterAttribute(
                new StringIdentifier("actor-stats"),
                new ActorStatFilterAttributeValueProvider(
                    _gameObjectManager,
                    _statCalculationService,
                    _statCalculationContextFactory),
                false);
        }
    }
}