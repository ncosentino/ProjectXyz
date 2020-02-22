using System.Linq;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class HasStatsStatCalculatorHandler : IStatCalculatorHandler
    {
        public CanCalculateStatDelegate CanCalculateStat { get; } =
            behaviors => behaviors.Has<IHasStatsBehavior>();

        public CalculateStatDelegate CalculateStat { get; } =
            (behaviors, enchantments, statId, context) =>
            {
                var currentContext = new StatCalculationContext(
                    context.Components,
                    context.Enchantments.Concat(enchantments));
                var statValue = behaviors
                    .GetOnly<IHasStatsBehavior>()
                    .GetStatValue(
                        currentContext,
                        statId);
                return statValue;
            };
    }
}
