using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Application.Shared.Enchantments;
using ClassLibrary1.Plugins.Api.Enchantments;

namespace ClassLibrary1.Plugins.Enchantments.OneShotNegation
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Constructors
        public Plugin(IEnchantmentPluginInitializationProvider enchantmentPluginInitializationProvider)
        {
            var statusNegations = new IStatusNegation[0];
            var enchantmentTypeCalculatorResultFactory = new EnchantmentTypeCalculatorResultFactory();

            EnchantmentTypeCalculator = new EnchantmentTypeCalculator(
                statusNegations,
                enchantmentTypeCalculatorResultFactory);
        }
        #endregion

        #region Properties
        public IEnchantmentTypeCalculator EnchantmentTypeCalculator { get; }
        #endregion
    }
}
