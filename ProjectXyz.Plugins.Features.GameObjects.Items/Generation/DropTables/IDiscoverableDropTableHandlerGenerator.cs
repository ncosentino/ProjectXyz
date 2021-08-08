using System;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public interface IDiscoverableDropTableHandlerGenerator : IDropTableHandlerGenerator
    {
        Type DropTableType { get; }
    }
}