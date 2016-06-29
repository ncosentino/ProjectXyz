using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculator : IStatCalculator
    {
        private readonly IStatDefinitionToCalculationLookup _statDefinitionToCalculationLookup;

        public StatCalculator(IStatDefinitionToCalculationLookup statDefinitionToCalculationLookup)
        {
            _statDefinitionToCalculationLookup = statDefinitionToCalculationLookup;
        }

        public double Calculate(
            IIdentifier statDefinitionId,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats)
        {
            var node = GetStatCalculationNode(
                statDefinitionId,
                baseStats);
            var value = node.GetValue();
            return value;
        }

        private IStatCalculationNode GetStatCalculationNode(
            IIdentifier statDefinitionId,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats)
        {
            IStatCalculationNode node;
            return baseStats.ContainsKey(statDefinitionId)
                ? new ValueStatCalculationNode(baseStats[statDefinitionId].Value)
                : _statDefinitionToCalculationLookup.GetCalculationNode(statDefinitionId);
        }
    }
}