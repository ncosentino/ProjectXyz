using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Enchantments.Stats
{
    public sealed class StatManagerFactory : IStatManagerFactory
    {
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> _statToEnchantmentContextConverter;

        public StatManagerFactory(
            IEnchantmentCalculator enchantmentCalculator,
            IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> statToEnchantmentContextConverter)
        {
            _enchantmentCalculator = enchantmentCalculator;
            _statToEnchantmentContextConverter = statToEnchantmentContextConverter;
        }

        public IStatManager Create(IMutableStatsProvider mutableStatsProvider) =>
            new StatManager(
                _enchantmentCalculator,
                mutableStatsProvider,
                _statToEnchantmentContextConverter);
    }
}