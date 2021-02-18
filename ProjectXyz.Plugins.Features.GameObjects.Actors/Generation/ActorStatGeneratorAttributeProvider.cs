using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatGeneratorAttributeProvider : IDiscoverableGeneratorContextAttributeProvider
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;

        public ActorStatGeneratorAttributeProvider(
            IGameObjectManager gameObjectManager,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory)
        {
            _gameObjectManager = gameObjectManager;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
        }

        public IEnumerable<IGeneratorAttribute> GetAttributes()
        {
            yield return new GeneratorAttribute(
                new StringIdentifier("actor-stats"),
                new ActorStatGeneratorAttributeValueProvider(
                    _gameObjectManager,
                    _statCalculationService,
                    _statCalculationContextFactory),
                false);
        }
    }
}