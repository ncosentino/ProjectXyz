using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
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