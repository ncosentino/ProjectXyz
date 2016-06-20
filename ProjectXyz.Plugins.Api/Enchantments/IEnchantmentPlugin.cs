using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Plugins.Api.Enchantments
{
    public interface IEnchantmentPlugin : IPlugin
    {
        IEnchantmentTypeCalculator EnchantmentTypeCalculator { get; }
    }
}
