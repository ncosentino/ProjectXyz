using System;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables
{
    public interface IDiscoverableDropTableHandlerGenerator : IDropTableHandlerGenerator
    {
        Type DropTableType { get; }
    }
}