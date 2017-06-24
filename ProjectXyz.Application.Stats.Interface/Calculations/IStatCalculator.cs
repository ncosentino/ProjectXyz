using System.Collections.Generic;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Stats.Interface.Calculations
{
    public interface IStatCalculator
    {
        double Calculate(
            IReadOnlyCollection<IStatExpressionInterceptor> statExpressionInterceptors,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId);
    }
}