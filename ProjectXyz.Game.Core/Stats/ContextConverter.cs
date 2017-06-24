using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class ContextConverter : IConvert<IStatCalculationContext, IEnchantmentCalculatorContext>
    {
        private readonly IInterval _zeroElapsedTime;

        public ContextConverter(IInterval zeroElapsedTime)
        {
            _zeroElapsedTime = zeroElapsedTime;
        }

        public IEnchantmentCalculatorContext Convert(IStatCalculationContext input)
        {
            return new EnchantmentCalculatorContext(
                input.Components,
                _zeroElapsedTime,
                input.Enchantments);
        }
    }
}