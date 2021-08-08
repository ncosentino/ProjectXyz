using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Linked;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked
{
    public sealed class WightedEntry : IWeightedEntry
    {
        public WightedEntry(
            double weight,
            IIdentifier dropTableId)
        {
            Weight = weight;
            DropTableId = dropTableId;
        }

        public double Weight { get; }

        public IIdentifier DropTableId { get; }
    }
}