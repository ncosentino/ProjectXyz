using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class StatManager : IStatManager
    {
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IMutableStatsProvider _mutableStatsProvider;
        private readonly IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> _statToEnchantmentContextConverter;

        public StatManager(
            IEnchantmentCalculator enchantmentCalculator,
            IMutableStatsProvider mutableStatsProvider,
            IConvert<IStatCalculationContext, IEnchantmentCalculatorContext> statToEnchantmentContextConverter)
        {
            _enchantmentCalculator = enchantmentCalculator;
            _mutableStatsProvider = mutableStatsProvider;
            _statToEnchantmentContextConverter = statToEnchantmentContextConverter;
        }

        public IReadOnlyDictionary<IIdentifier, double> BaseStats => _mutableStatsProvider.Stats;

        public double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId)
        {
            var enchantmentCalculationContext = _statToEnchantmentContextConverter.Convert(statCalculationContext);
            var value = _enchantmentCalculator.Calculate(
                enchantmentCalculationContext,
                _mutableStatsProvider.Stats,
                statDefinitionId);
            return value;
        }

        public void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback) =>
            _mutableStatsProvider.UsingMutableStats(callback);
    }
}