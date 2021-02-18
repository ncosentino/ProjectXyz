using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatGeneratorAttributeValueProvider : IGeneratorAttributeValue
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;

        public ActorStatGeneratorAttributeValueProvider(
            IGameObjectManager gameObjectManager,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory)
        {
            _gameObjectManager = gameObjectManager;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
        }

        public bool TryGetActorStat(
            IIdentifier actorId,
            IIdentifier statDefinitionId,
            out double statValue)
        {
            statValue = 0;

            var actor = _gameObjectManager
                .GameObjects
                .FirstOrDefault(x => x
                .Get<IIdentifierBehavior>()
                .Any(b => b.Id.Equals(actorId)));
            if (actor == null)
            {
                return false;
            }

            var context = _statCalculationContextFactory.Create(
                Enumerable.Empty<IComponent>(),
                Enumerable.Empty<IEnchantment>());

            statValue = _statCalculationService.GetStatValue(
                actor,
                statDefinitionId,
                context);
            return true;
        }
    }
}