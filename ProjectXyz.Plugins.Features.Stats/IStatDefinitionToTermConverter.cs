using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public interface IStatDefinitionToTermConverter : IReadOnlyDictionary<IIdentifier, string>
    {
    }
}
