using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats
{
    public interface IStat
    {
        double Value { get; }

        IIdentifier StatDefinitionId { get; }
    }
}