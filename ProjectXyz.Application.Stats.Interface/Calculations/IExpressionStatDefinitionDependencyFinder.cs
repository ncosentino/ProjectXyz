using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IExpressionStatDefinitionDependencyFinder
    {
        IReadOnlyCollection<IIdentifier> FindDependencies(
            IStatDefinitionToTermConverter statDefinitionToTermConverter,
            string expression);
    }
}