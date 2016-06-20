using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Shared.Enchantments;
using ProjectXyz.Plugins.Api.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegation
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
