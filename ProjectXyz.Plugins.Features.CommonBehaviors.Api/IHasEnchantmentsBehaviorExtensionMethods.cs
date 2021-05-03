using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public static class IHasEnchantmentsBehaviorExtensionMethods
    {
        public static void AddEnchantments(
            this IHasEnchantmentsBehavior @this,
            IGameObject enchantment,
            params IGameObject[] others) => @this.AddEnchantments(
                others == null
                ? new[] { enchantment }
                : new[] { enchantment }.Concat(others));

        public static void RemoveEnchantments(
            this IHasEnchantmentsBehavior @this,
            IGameObject enchantment,
            params IGameObject[] others) => @this.RemoveEnchantments(
                others == null
                ? new[] { enchantment }
                : new[] { enchantment }.Concat(others));
    }
}
