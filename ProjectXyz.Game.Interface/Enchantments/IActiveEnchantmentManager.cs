using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Game.Interface.Enchantments
{
    public interface IActiveEnchantmentManager : IEnchantmentProvider
    {
        void Add(IEnumerable<IEnchantment> enchantment);

        void Remove(IEnumerable<IEnchantment> enchantment);
    }
}