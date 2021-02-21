using System.Collections.Generic;

using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasEnchantmentsBehavior : IHasReadOnlyEnchantmentsBehavior
    {
        void AddEnchantments(IEnumerable<IEnchantment> enchantments);

        void RemoveEnchantments(IEnumerable<IEnchantment> enchantments);
    }
}