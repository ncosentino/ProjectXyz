using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats
{
    public interface IStatDefinitionToTermConverter : IReadOnlyDictionary<IIdentifier, string>
    {
    }
}
