using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class Buffable :
        BaseBehavior,
        IBuffable
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public Buffable(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public void AddEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _activeEnchantmentManager.Add(enchantments);
        }
    }
}
