using System.Data;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Shared.Enchantments;
using ProjectXyz.Application.Shared.Stats;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Plugins.Api.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Constructors
        public Plugin(IEnchantmentPluginInitializationProvider enchantmentPluginInitializationProvider)
        {
            var statFactory = new StatFactory();
            var statCollectionFactory = new StatCollectionFactory();
            var expressionEvaluator = new ExpressionEvaluator(new DataTableExpressionEvaluator(new DataTable()).Evaluate);
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
