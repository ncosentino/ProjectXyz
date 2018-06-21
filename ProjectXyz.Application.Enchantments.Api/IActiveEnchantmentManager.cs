using System.Collections.Generic;

namespace ProjectXyz.Api.Enchantments
{
    public interface IActiveEnchantmentManager : IEnchantmentProvider
    {
        void Add(IEnumerable<IEnchantment> enchantment);

        void Remove(IEnumerable<IEnchantment> enchantment);
    }
}