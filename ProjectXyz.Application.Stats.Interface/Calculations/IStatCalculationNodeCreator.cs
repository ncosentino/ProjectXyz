using System.Collections.Generic;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Stats.Interface.Calculations
{
    public interface IStatCalculationNodeCreator
    {
        IStatCalculationNode Create(
            IReadOnlyCollection<IStatExpressionInterceptor> statExpressionInterceptors,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId);
    }
}