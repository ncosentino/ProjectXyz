using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Game.Interface.Enchantments
{
    public interface IActiveEnchantmentManager : IEnchantmentProvider
    {
        void Add(IEnchantment enchantment);

        void Remove(IEnchantment enchantment);
    }
}