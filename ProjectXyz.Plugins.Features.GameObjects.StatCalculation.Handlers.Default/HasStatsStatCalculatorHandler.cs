using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class HasStatsStatCalculatorHandler : IDiscoverableStatCalculatorHandler
    {
        private readonly IComponentsHandlerFacade _componentsHandlerFacade;

        public HasStatsStatCalculatorHandler(IComponentsHandlerFacade componentsHandlerFacade)
        {
            _componentsHandlerFacade = componentsHandlerFacade;
            CalculateStat =
            (behaviors, enchantments, statId, context) =>
            {
                var components = context.Components.ToList();
                components.AddRange(_componentsHandlerFacade.HandleComponents(
                    behaviors,
                    context.Components));

                var currentContext = new StatCalculationContext(
                    components,
                    context.Enchantments.Concat(enchantments));
                var statValue = behaviors
                    .GetOnly<IHasStatsBehavior>()
                    .GetStatValue(
                        currentContext,
                        statId);
                return statValue;
            };
        }

        public CanCalculateStatDelegate CanCalculateStat { get; } =
            behaviors => behaviors.Has<IHasStatsBehavior>();

        public CalculateStatDelegate CalculateStat { get; }
    }
}
