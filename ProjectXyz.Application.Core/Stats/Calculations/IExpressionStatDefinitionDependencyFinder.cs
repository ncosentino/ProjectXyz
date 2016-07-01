using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public interface IExpressionStatDefinitionDependencyFinder
    {
        IReadOnlyCollection<IIdentifier> FindDependencies(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            string expression);
    }
}