using System.Linq;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public static class IHasEnchantmentsBehaviorExtensionMethods
    {
        public static void AddEnchantments(
            this IHasEnchantmentsBehavior @this,
            IEnchantment enchantment,
            params IEnchantment[] others) => @this.AddEnchantments(
                others == null
                ? new[] { enchantment }
                : new[] { enchantment }.Concat(others));

        public static void RemoveEnchantments(
            this IHasEnchantmentsBehavior @this,
            IEnchantment enchantment,
            params IEnchantment[] others) => @this.RemoveEnchantments(
                others == null
                ? new[] { enchantment }
                : new[] { enchantment }.Concat(others));
    }
}
