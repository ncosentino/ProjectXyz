using ClassLibrary1.Application.Interface.Enchantments;

namespace ClassLibrary1.Plugins.Api.Enchantments
{
    public interface IEnchantmentPlugin : IPlugin
    {
        IEnchantmentTypeCalculator EnchantmentTypeCalculator { get; }
    }
}
