using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculator : IStatCalculator
    {
        private readonly IReadOnlyDictionary<IIdentifier, IStatCalculationNode> _statDefinitionToNodeMapping;

        public StatCalculator(IReadOnlyDictionary<IIdentifier, IStatCalculationNode> statDefinitionToNodeMapping)
        {
            _statDefinitionToNodeMapping = statDefinitionToNodeMapping;
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
                : _statDefinitionToNodeMapping.TryGetValue(statDefinitionId, out node)
                    ? node
                    : new ValueStatCalculationNode(0);
        }
    }
}