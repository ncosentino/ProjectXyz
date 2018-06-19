using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class EnchantableBehavior :
        BaseBehavior,
        IEnchantableBehavior
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public EnchantableBehavior(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public void Enchant(IEnumerable<IEnchantment> enchantments) => _activeEnchantmentManager.Add(enchantments);
    }
}