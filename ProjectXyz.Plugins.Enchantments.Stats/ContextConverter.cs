using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Enchantments.Stats
{
    public sealed class ContextConverter : IConvert<IStatCalculationContext, IEnchantmentCalculatorContext>
    {
        private readonly IEnchantmentCalculatorContextFactory _enchantmentCalculatorContextFactory;
        private readonly IInterval _zeroElapsedTime;

        public ContextConverter(
            IEnchantmentCalculatorContextFactory enchantmentCalculatorContextFactory,
            IInterval zeroElapsedTime)
        {
            _enchantmentCalculatorContextFactory = enchantmentCalculatorContextFactory;
            _zeroElapsedTime = zeroElapsedTime;
        }

        public IEnchantmentCalculatorContext Convert(IStatCalculationContext input)
        {
            return _enchantmentCalculatorContextFactory.CreateEnchantmentCalculatorContext(
                _zeroElapsedTime,
                input.Enchantments,
                input.Components);
        }
    }
}