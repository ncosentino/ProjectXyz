using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Default
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
