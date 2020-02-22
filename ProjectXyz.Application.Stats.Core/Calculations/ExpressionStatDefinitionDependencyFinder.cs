using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.Stats.Calculations
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