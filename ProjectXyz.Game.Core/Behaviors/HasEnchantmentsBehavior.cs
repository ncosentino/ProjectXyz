using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasEnchantmentsBehavior :
        BaseBehavior,
        IHasEnchantmentsBehavior
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public HasEnchantmentsBehavior(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public IReadOnlyCollection<IEnchantment> Enchantments => _activeEnchantmentManager.Enchantments;
    }
}