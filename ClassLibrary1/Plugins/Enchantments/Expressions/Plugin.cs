using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Application.Shared.Enchantments;
using ClassLibrary1.Application.Shared.Stats;
using ClassLibrary1.Plugins.Api.Enchantments;

namespace ClassLibrary1.Plugins.Enchantments.Expressions
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Constructors
        public Plugin(IEnchantmentPluginInitializationProvider enchantmentPluginInitializationProvider)
        {
            var statFactory = new StatFactory();
            var statCollectionFactory = new StatCollectionFactory();
            var expressionEvaluator = new ExpressionEvaluator(new DataTableExpressionEvaluator().Evaluate);
            var enchantmentTypeCalculatorResultFactory = new EnchantmentTypeCalculatorResultFactory();

            EnchantmentTypeCalculator = new EnchantmentTypeCalculator(
                statFactory,
                expressionEvaluator,
                enchantmentTypeCalculatorResultFactory,
                statCollectionFactory,
                enchantmentPluginInitializationProvider.WeatherManager);
        }
        #endregion

        #region Properties
        public IEnchantmentTypeCalculator EnchantmentTypeCalculator { get; }
        #endregion
    }
}
