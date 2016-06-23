using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatCalculator
    {
        double Calculate(
            IIdentifier statDefinitionId,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats);
    }
}