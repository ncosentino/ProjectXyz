using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStat
    {
        double Value { get; }

        IIdentifier StatDefinitionId { get; }
    }
}