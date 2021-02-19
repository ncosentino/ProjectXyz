using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Behaviors;
using ProjectXyz.Shared.Game.GameObjects.Enchantments;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class HasEquipmentGetEnchantmentsHandler : IGetEnchantmentsHandler
    {
        public HasEquipmentGetEnchantmentsHandler(
            ITargetNavigator targetNavigator,
            IStatDefinitionToTermConverter statDefinitionToTermConverter)
        {
            GetEnchantments = (statVisitor, enchantmentVisitor, behaviors, target, statId, context) =>
            {
                var hasEquipment = behaviors.GetOnly<IHasEquipmentBehavior>();
                var equipment = hasEquipment
                    ?.SupportedEquipSlotIds
                    .Select(slot =>
                    {
                        return hasEquipment.TryGet(slot, out var equipped)
                            ? equipped
                            : null;
                    })
                    .Where(x => x != null)
                    .ToArray()
                    ?? new ICanBeEquippedBehavior[0];

                var equipmentTarget = targetNavigator.NavigateDown(target);
                var equipmentOwnerEnchantments = equipment
                    .SelectMany(x => enchantmentVisitor.GetEnchantments(
                        x.Owner,
                        y => y.Equals(behaviors), // TODO: handle visited
                        equipmentTarget,
                        statId,
                        context))
                    .ToArray();

                var equipmentStats = equipment
                    .Where(x => x.Owner.Has<IHasStatsBehavior>())
                    .Select(x => statVisitor.GetStatValue(
                        x.Owner,
                        statId,
                        context))
                    .ToArray();
                var statTerm = statDefinitionToTermConverter[statId];
                var baseEquipmentStatsEnchantment = new Enchantment(
                    new IBehavior[]
                    {
                        new HasStatDefinitionIdBehavior()
                        {
                            StatDefinitionId = statId,
                        },
                        new EnchantmentExpressionBehavior(
                            new CalculationPriority<int>(-1),
                            $"{statTerm} + {equipmentStats.Sum()}")
                    });
                var allEquipmentEnchantments = equipmentOwnerEnchantments
                    .AppendSingle(baseEquipmentStatsEnchantment)
                    .ToArray();
                return allEquipmentEnchantments;
            };
        }

        public CanGetEnchantmentsDelegate CanGetEnchantments { get; } =
            behaviors => behaviors.Has<IHasEquipmentBehavior>();

        public GetEnchantmentsDelegate GetEnchantments { get; }
    }
}
