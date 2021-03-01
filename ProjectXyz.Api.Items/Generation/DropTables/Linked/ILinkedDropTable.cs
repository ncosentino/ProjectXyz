using System.Collections.Generic;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables.Linked
{
    public interface ILinkedDropTable : IDropTable
    {
        IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}