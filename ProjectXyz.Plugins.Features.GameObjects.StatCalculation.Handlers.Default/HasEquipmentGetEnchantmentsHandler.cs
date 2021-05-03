using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class HasEquipmentGetEnchantmentsHandler : IDiscoverableGetEnchantmentsHandler
    {
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;

        public HasEquipmentGetEnchantmentsHandler(
            IEnchantmentFactory enchantmentFactory,
            ITargetNavigator targetNavigator,
            IStatDefinitionToTermConverter statDefinitionToTermConverter,
            ICalculationPriorityFactory calculationPriorityFactory)
        {
            _enchantmentFactory = enchantmentFactory;
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
                    .ToList();

                var equipmentStats = equipment
                    .Where(x => x.Owner.Has<IHasStatsBehavior>())
                    .Select(x => statVisitor.GetStatValue(
                        x.Owner,
                        statId,
                        context))
                    .ToArray();
                var enchantmentValue = equipmentStats.Sum();

                var allEquipmentEnchantments = equipmentOwnerEnchantments;
                if (enchantmentValue != 0)
                {
                    var statTerm = statDefinitionToTermConverter[statId];
                    var baseEquipmentStatsEnchantment = _enchantmentFactory.Create(
                        new IBehavior[]
                        {
                        new EnchantmentTargetBehavior(equipmentTarget),
                        new HasStatDefinitionIdBehavior()
                        {
                            StatDefinitionId = statId,
                        },
                        new EnchantmentExpressionBehavior(
                            _calculationPriorityFactory.Create<int>(-1),
                            $"{statTerm} + {enchantmentValue}")
                        });
                    allEquipmentEnchantments.Add(baseEquipmentStatsEnchantment);
                }

                return allEquipmentEnchantments;
            };
            _calculationPriorityFactory = calculationPriorityFactory;
        }

        public CanGetEnchantmentsDelegate CanGetEnchantments { get; } =
            behaviors => behaviors.Has<IHasEquipmentBehavior>();

        public GetEnchantmentsDelegate GetEnchantments { get; }
    }
}
