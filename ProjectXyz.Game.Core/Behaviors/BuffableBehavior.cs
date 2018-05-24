using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
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
