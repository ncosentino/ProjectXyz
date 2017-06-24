using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class StatManager : IStatManager
    {
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IStatsProvider _statsProvider;
        private readonly IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> _statToEnchantmentContextConverter;

        public StatManager(
            IEnchantmentCalculator enchantmentCalculator,
            IStatsProvider statsProvider,
            IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> statToEnchantmentContextConverter)
        {
            _enchantmentCalculator = enchantmentCalculator;
            _statsProvider = statsProvider;
            _statToEnchantmentContextConverter = statToEnchantmentContextConverter;
        }

        public double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId)
        {
            var enchantmentCalculationContext = _statToEnchantmentContextConverter.Convert(statCalculationContext);
            var value = _enchantmentCalculator.Calculate(
                enchantmentCalculationContext,
                _statsProvider.Stats,
                statDefinitionId);
            return value;
        }
    }
}