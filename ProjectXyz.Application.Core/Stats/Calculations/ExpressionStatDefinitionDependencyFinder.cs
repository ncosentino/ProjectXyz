using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class ExpressionStatDefinitionDependencyFinder : IExpressionStatDefinitionDependencyFinder
    {
        public IReadOnlyCollection<IIdentifier> FindDependencies(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            string expression)
        {
            var dependencies = statDefinitionIdToTermMapping
                .Where(x => expression.Contains(x.Value))
                .Select(x => x.Key)
                .ToArray();
            return dependencies;
        }
    }
}