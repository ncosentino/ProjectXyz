using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class BuffableBehavior :
        BaseBehavior,
        IBuffableBehavior
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public BuffableBehavior(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public void AddEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _activeEnchantmentManager.Add(enchantments);
        }
    }
}
