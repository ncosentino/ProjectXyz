using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public interface IStatDefinitionToTermMapping
    {
        IIdentifier StatDefinitionId { get; }

        string Term { get; }
    }
}