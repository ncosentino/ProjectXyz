using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculator : IStatCalculator
    {
        private readonly IStatCalculationNodeCreator _statCalculationNodeCreator;

        public StatCalculator(IStatCalculationNodeCreator statCalculationNodeCreator)
        {
            _statCalculationNodeCreator = statCalculationNodeCreator;
        }

        public double Calculate(
            IStatExpressionInterceptor statExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            var statCalculationNode = _statCalculationNodeCreator.Create(
                statExpressionInterceptor,
                baseStats,
                statDefinitionId);
            var value = statCalculationNode.GetValue();
            return value;
        }
    }
}