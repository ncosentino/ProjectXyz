using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked
{
    public interface IWeightedEntry
    {
        double Weight { get; }

        IIdentifier DropTableId { get; }
    }
}