using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats
{
    public interface IStatDefinitionToTermMapping
    {
        IIdentifier StatDefinitionId { get; }

        string Term { get; }
    }
}