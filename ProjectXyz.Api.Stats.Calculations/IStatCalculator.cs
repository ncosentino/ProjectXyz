using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStatCalculator
    {
        double Calculate(
            IReadOnlyCollection<IStatExpressionInterceptor> statExpressionInterceptors,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId);
    }
}