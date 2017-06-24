using System.Collections.Generic;
using ProjectXyz.Api.Stats.Calculations;
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
            IReadOnlyCollection<IStatExpressionInterceptor> statExpressionInterceptors,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            var statCalculationNode = _statCalculationNodeCreator.Create(
                statExpressionInterceptors,
                baseStats,
                statDefinitionId);
            var value = statCalculationNode.GetValue();
            return value;
        }
    }
}