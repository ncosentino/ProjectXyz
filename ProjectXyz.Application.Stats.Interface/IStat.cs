using ProjectXyz.Api.Framework;

namespace ProjectXyz.Application.Stats.Interface
{
    public interface IStat
    {
        double Value { get; }

        IIdentifier StatDefinitionId { get; }
    }
}