using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;

namespace ProjectXyz.Game.Interface.Enchantments
{
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