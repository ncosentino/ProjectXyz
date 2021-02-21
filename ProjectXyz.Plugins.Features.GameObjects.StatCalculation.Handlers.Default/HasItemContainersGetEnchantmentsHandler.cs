using System.Linq;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class HasItemContainersGetEnchantmentsHandler : IDiscoverableGetEnchantmentsHandler
    {
        public HasItemContainersGetEnchantmentsHandler(ITargetNavigator targetNavigator)
        {
            GetEnchantments = (statVisitor, enchantmentVisitor, behaviors, target, statId, context) =>
            {
                var itemTarget = targetNavigator.NavigateDown(targetNavigator.NavigateDown(target));
                var itemContainers = behaviors
                   .Get<IReadOnlyHasItemContainersBehavior>()
                   .SelectMany(container => container.ItemContainers)
                   .ToArray();
                var containerItems = itemContainers
                    .SelectMany(container => container.Items)
                    .ToArray()
                    ?? new IGameObject[0];
                var containerItemsOwnerEnchantments = containerItems
                    .SelectMany(x => enchantmentVisitor.GetEnchantments(
                        x,
                        _ => false, // TODO: handle visited
                        itemTarget,
                        statId,
                        context))
                    .ToArray();
                return containerItemsOwnerEnchantments;
            };
        }

        public CanGetEnchantmentsDelegate CanGetEnchantments { get; } =
            behaviors => behaviors.Has<IReadOnlyHasItemContainersBehavior>();

        public GetEnchantmentsDelegate GetEnchantments { get; }
    }
}
