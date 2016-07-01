using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IExpressionStatDefinitionDependencyFinder
    {
        IReadOnlyCollection<IIdentifier> FindDependencies(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            string expression);
    }
}