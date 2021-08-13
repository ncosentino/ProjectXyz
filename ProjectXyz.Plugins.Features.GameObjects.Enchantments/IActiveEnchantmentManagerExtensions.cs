using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments
{
    public static class IActiveEnchantmentManagerExtensions
    {
        public static void Add(
            this IActiveEnchantmentManager activeEnchantmentManager,
            IGameObject enchantment)
        {
            activeEnchantmentManager.Add(enchantment.Yield());
        }

        public static void Remove(
            this IActiveEnchantmentManager activeEnchantmentManager,
            IGameObject enchantment)
        {
            activeEnchantmentManager.Remove(enchantment.Yield());
        }
    }
}