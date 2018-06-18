using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Game.Core.Stats
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