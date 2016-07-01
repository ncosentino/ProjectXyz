using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatCalculationNodeCreator
    {
        IStatCalculationNode Create(
            IStatExpressionInterceptor statExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IIdentifier statDefinitionId);
    }
}