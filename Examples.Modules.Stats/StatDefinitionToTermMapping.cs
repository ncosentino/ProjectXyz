using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace Examples.Modules.Stats
{
    public sealed class StatDefinitionToTermMapping : IStatDefinitionToTermMapping
    {
        public StatDefinitionToTermMapping(IIdentifier stateDefinitionId, string term)
        {
            StatDefinitionId = stateDefinitionId;
            Term = term;
        }

        public IIdentifier StatDefinitionId { get; }

        public string Term { get; }
    }
}
