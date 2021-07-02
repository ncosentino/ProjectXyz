using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Shared.Framework;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class EnchantTargetsGeneratorComponent : IGeneratorComponent
    {
        public EnchantTargetsGeneratorComponent(
            params string[] enchantmentIds)
        {
            EnchantmentDefinitionIds = enchantmentIds
                .Select(x => new StringIdentifier(x))
                .ToArray();
        }

        public IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }
    }

    public sealed class EnchantTargetsConverter : ISkillEffectBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is EnchantTargetsGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var enchantComponent = (EnchantTargetsGeneratorComponent)component;

            return new ApplyEnchantmentsBehavior(
                enchantComponent.EnchantmentDefinitionIds);
        }
    }
}
