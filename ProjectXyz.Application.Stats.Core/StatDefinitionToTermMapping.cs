using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Application.Stats.Core
{

    public sealed class StatDefinitionToTermMapping : IStatDefinitionToTermMapping
    {
        public StatDefinitionToTermMapping(IIdentifier stateDefinitionId, string term)
        {
            StateDefinitionId = stateDefinitionId;
            Term = term;
        }

        public IIdentifier StateDefinitionId { get; }

        public string Term { get; }
    }
}
