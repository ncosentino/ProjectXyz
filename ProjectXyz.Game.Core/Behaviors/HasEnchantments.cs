using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasEnchantments :
        BaseBehavior,
        IHasEnchantments
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public HasEnchantments(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public IReadOnlyCollection<IEnchantment> Enchantments => _activeEnchantmentManager.Enchantments;
    }
}