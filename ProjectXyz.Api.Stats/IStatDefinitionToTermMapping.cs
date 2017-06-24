using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Stats
{
    public interface IStatDefinitionToTermMapping
    {
        IIdentifier StateDefinitionId { get; }

        string Term { get; }
    }
}