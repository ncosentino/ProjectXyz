using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class HasEnchantmentsGetEnchantmentsHandler : IDiscoverableGetEnchantmentsHandler
    {
        public HasEnchantmentsGetEnchantmentsHandler(ITargetNavigator targetNavigator)
        {
            GetEnchantments = (statVisitor, enchantmentVisitor, behaviors, target, statId, context) =>
            {
                var enchantments = behaviors
                    .Get<IReadOnlyHasEnchantmentsBehavior>()
                    ?.SelectMany(x => x.Enchantments.Select(enchantment =>
                    {
                        Contract.RequiresNotNull(
                            enchantment,
                            () => $"'{x.Owner}.{x}' (id={x.Owner.GetOnly<IReadOnlyIdentifierBehavior>().Id}) " +
                            $"returned a null enchantment as part of its " +
                            $"collection which is against out contract.");
                        return enchantment;
                    }))
                    .ToArray()
                    ?? new IGameObject[0];
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
            behaviors => behaviors.Has<IReadOnlyHasEnchantmentsBehavior>();

        public GetEnchantmentsDelegate GetEnchantments { get; }
    }
}
