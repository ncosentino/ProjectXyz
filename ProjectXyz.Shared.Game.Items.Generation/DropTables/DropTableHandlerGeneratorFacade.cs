using System;
using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class DropTableHandlerGeneratorFacade : IDropTableHandlerGeneratorFacade
    {
        private readonly Dictionary<Type, IDropTableHandlerGenerator> _mapping;

        public DropTableHandlerGeneratorFacade()
        {
            _mapping = new Dictionary<Type, IDropTableHandlerGenerator>();
        }

        public IEnumerable<IGameObject> GenerateLoot(
            IDropTable dropTable,
            IGeneratorContext generatorContext)
        {
            if (!_mapping.TryGetValue(
                dropTable.GetType(),
                out var dropTableHandlerGenerator))
            {
                throw new InvalidOperationException(
                    $"No supported dorp table handler generator for type '{dropTable.GetType()}'.");
            }

            var generated = dropTableHandlerGenerator.GenerateLoot(
                dropTable,
                generatorContext);
            return generated;
        }

        public void Register(
            Type dropTableType,
            IDropTableHandlerGenerator dropTableHandlerGenerator)
        {
            _mapping.Add(dropTableType, dropTableHandlerGenerator);
        }
    }
}