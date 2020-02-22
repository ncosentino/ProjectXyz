using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class HasEnchantmentsGetEnchantmentsHandler : IGetEnchantmentsHandler
    {
        public HasEnchantmentsGetEnchantmentsHandler(ITargetNavigator targetNavigator)
        {
            GetEnchantments = (statVisitor, enchantmentVisitor, behaviors, target, statId, context) =>
            {
                var enchantments = behaviors
                    .Get<IHasEnchantmentsBehavior>()
                    ?.SelectMany(x => x.Enchantments)
                    .ToArray()
                    ?? new IEnchantment[0];
                var selfEnchantments = enchantments
                    .Where(x => targetNavigator.AreTargetsEqual(x
                        .GetOnly<IEnchantmentTargetBehavior>()
                        .Target,
                        target))
                    .ToArray();
                var otherEnchantments = enchantmentVisitor.GetEnchantments(
                    behaviors,
                    x => x.Equals(behaviors), // TODO: handle visited
                    targetNavigator.NavigateDown(target),
                    statId,
                    context);
                var allEnchantments = selfEnchantments
                    .Concat(otherEnchantments)
                    .ToArray();
                return allEnchantments;
            };
        }

        public CanGetEnchantmentsDelegate CanGetEnchantments { get; } =
            behaviors => behaviors.Has<IHasEnchantmentsBehavior>();

        public GetEnchantmentsDelegate GetEnchantments { get; }
    }
}
