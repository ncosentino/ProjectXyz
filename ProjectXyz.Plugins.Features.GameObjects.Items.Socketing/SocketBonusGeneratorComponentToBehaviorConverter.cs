using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class SocketBonusGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IEnchantmentLoader _enchantmentLoader;

        public SocketBonusGeneratorComponentToBehaviorConverter(IEnchantmentLoader enchantmentLoader)
        {
            _enchantmentLoader = enchantmentLoader;
        }

        public Type ComponentType => typeof(SocketBonusGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors, 
            IGeneratorComponent generatorComponent)
        {
            var castedGeneratorComponent = (SocketBonusGeneratorComponent)generatorComponent;
            var behavior = new SocketBonusBehavior(
                castedGeneratorComponent.Filters,
                _enchantmentLoader.LoadForEnchantmenDefinitionIds(castedGeneratorComponent.EnchantmentDefinitionIds));
            yield return behavior;
        }
    }
}