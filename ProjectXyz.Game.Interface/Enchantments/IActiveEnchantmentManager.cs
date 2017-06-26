using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Game.Interface.Enchantments
{
    public interface IActiveEnchantmentManager : IEnchantmentProvider
    {
        void Add(IEnumerable<IEnchantment> enchantment);

        void Remove(IEnumerable<IEnchantment> enchantment);
    }

    public static class IActiveEnchantmentManagerExtensions
    {
        public static void Add(
            this IActiveEnchantmentManager activeEnchantmentManager,
            IEnchantment enchantment)
        {
            activeEnchantmentManager.Add(enchantment.Yield());
        }

        public static void Remove(
            this IActiveEnchantmentManager activeEnchantmentManager,
            IEnchantment enchantment)
        {
            activeEnchantmentManager.Remove(enchantment.Yield());
        }
    }
}