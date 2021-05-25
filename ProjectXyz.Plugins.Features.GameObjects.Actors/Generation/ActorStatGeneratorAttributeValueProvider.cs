using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatFilterAttributeValueProvider : IFilterAttributeValue
    {
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;

        public ActorStatFilterAttributeValueProvider(
            IMapGameObjectManager mapGameObjectManager,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory)
        {
            _mapGameObjectManager = mapGameObjectManager;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
        }

        public bool TryGetActorStat(
            IIdentifier actorId,
            IIdentifier statDefinitionId,
            out double statValue)
        {
            statValue = 0;

            var actor = _mapGameObjectManager
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
                Enumerable.Empty<IGameObject>());

            statValue = _statCalculationService.GetStatValue(
                actor,
                statDefinitionId,
                context);
            return true;
        }
    }
}