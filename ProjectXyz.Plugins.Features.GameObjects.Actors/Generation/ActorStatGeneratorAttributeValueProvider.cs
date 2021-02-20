using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatFilterAttributeValueProvider : IFilterAttributeValue
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;

        public ActorStatFilterAttributeValueProvider(
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