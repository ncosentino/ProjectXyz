using ProjectXyz.Application.Enchantments.Api;

namespace ProjectXyz.Application.Enchantments.Interface
{
    public interface IActiveEnchantmentManager : IEnchantmentProvider
    {
        void Add(IEnchantment enchantment);

        void Remove(IEnchantment enchantment);
    }
}