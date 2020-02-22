using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStatDefinitionToCalculationConverter : IReadOnlyDictionary<IIdentifier, string>
    {
    }
}
