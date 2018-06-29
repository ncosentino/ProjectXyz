using System;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables
{
    public interface IDropTableHandlerGeneratorRegistrar
    {
        void Register(
            Type dropTableType,
            IDropTableHandlerGenerator dropTableHandlerGenerator);
    }
}