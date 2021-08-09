using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public interface IStat
    {
        double Value { get; }

        IIdentifier StatDefinitionId { get; }
    }
}