using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IActiveEnchantmentManager : IEnchantmentProvider
    {
        void Add(IEnchantment enchantment);

        void Remove(IEnchantment enchantment);
    }
}