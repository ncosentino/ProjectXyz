using System.Collections.Generic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
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

        public void AddEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _activeEnchantmentManager.Add(enchantments);
        }

        public void RemoveEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _activeEnchantmentManager.Remove(enchantments);
        }
    }
}
