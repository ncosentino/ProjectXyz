using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Stats;

namespace ProjectXyz.Plugins.Features.Stats.Calculations
{
    public interface IExpressionStatDefinitionDependencyFinder
    {
        IReadOnlyCollection<IIdentifier> FindDependencies(
            IStatDefinitionToTermConverter statDefinitionToTermConverter,
            string expression);
    }
}