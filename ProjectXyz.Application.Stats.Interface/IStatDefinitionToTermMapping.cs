using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats
{
    public interface IStatDefinitionToTermMapping
    {
        IIdentifier StateDefinitionId { get; }

        string Term { get; }
    }
}