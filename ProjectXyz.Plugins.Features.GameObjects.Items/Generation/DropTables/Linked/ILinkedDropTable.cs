﻿using System.Collections.Generic;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Linked
{
    public interface ILinkedDropTable : IDropTable
    {
        IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}