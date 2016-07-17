using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IActiveEnchantmentManager
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }

        void Add(IEnchantment enchantment);

        void Remove(IEnchantment enchantment);
    }
}