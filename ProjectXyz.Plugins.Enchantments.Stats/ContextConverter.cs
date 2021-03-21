using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Enchantments.Stats
{
    public sealed class ContextConverter : IConvert<IStatCalculationContext, IEnchantmentCalculatorContext>
    {
        private readonly IEnchantmentCalculatorContextFactory _enchantmentCalculatorContextFactory;

        public ContextConverter(IEnchantmentCalculatorContextFactory enchantmentCalculatorContextFactory)
        {
            _enchantmentCalculatorContextFactory = enchantmentCalculatorContextFactory;
        }

        public IEnchantmentCalculatorContext Convert(IStatCalculationContext input)
        {
            return _enchantmentCalculatorContextFactory.CreateEnchantmentCalculatorContext(
                0,
                input.Enchantments,
                input.Components);
        }
    }
}