using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Features.Stats.Default.Calculations
{
    public sealed class ExpressionStatDefinitionDependencyFinder : IExpressionStatDefinitionDependencyFinder
    {
        public IReadOnlyCollection<IIdentifier> FindDependencies(
            IStatDefinitionToTermConverter statDefinitionToTermConverter,
            string expression)
        {
            var dependencies = statDefinitionToTermConverter
                .Where(x => expression.Contains(x.Value))
                .Select(x => x.Key)
                .ToArray();
            return dependencies;
        }
    }
}