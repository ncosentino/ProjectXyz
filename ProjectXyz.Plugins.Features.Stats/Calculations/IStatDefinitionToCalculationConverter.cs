using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Calculations
{
    public interface IStatDefinitionToCalculationConverter : IReadOnlyDictionary<IIdentifier, string>
    {
    }
}
