using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using ProjectXyz.Shared.Framework;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class PassiveEnchantmentGeneratorComponent : IGeneratorComponent
    {
        public PassiveEnchantmentGeneratorComponent(
            params string[] enchantmentIds)
        {
            EnchantmentDefinitionIds = enchantmentIds
                .Select(x => new StringIdentifier(x))
                .ToArray();
        }

        public IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }
    }

    public sealed class PassiveEnchantmentConverter : IBehaviorConverter
    {
        private readonly IEnchantmentLoader _enchantmentLoader;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;

        public PassiveEnchantmentConverter(
            IEnchantmentLoader enchantmentLoader,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory)
        {
            _enchantmentLoader = enchantmentLoader;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
        }

        public bool CanConvert(IGeneratorComponent component)
        {
            return component is PassiveEnchantmentGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var passiveEnchantComponent = (PassiveEnchantmentGeneratorComponent)component;

            var behavior = _hasEnchantmentsBehaviorFactory.Create();

            var enchantments = _enchantmentLoader.LoadForEnchantmenDefinitionIds(
                passiveEnchantComponent.EnchantmentDefinitionIds);

            behavior.AddEnchantments(enchantments);

            return behavior;
        }
    }
}
