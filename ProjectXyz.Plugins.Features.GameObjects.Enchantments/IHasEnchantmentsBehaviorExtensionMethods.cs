using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments
{
    public static class IHasEnchantmentsBehaviorExtensionMethods
    {
        public static Task AddEnchantmentsAsync(
            this IHasEnchantmentsBehavior @this,
            IGameObject enchantment,
            params IGameObject[] others) => @this.AddEnchantmentsAsync(
                others == null
                ? new[] { enchantment }
                : new[] { enchantment }.Concat(others));

        public static Task RemoveEnchantmentsAsync(
            this IHasEnchantmentsBehavior @this,
            IGameObject enchantment,
            params IGameObject[] others) => @this.RemoveEnchantmentsAsync(
                others == null
                ? new[] { enchantment }
                : new[] { enchantment }.Concat(others));
    }
}
