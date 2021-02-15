using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item
{
    public sealed class ItemDropTableHandlerGenerator : IDiscoverableDropTableHandlerGenerator
    {
        private readonly IItemGeneratorFacade _itemGeneratorFacade;
        private readonly IGeneratorContextFactory _generatorContextFactory;

        public ItemDropTableHandlerGenerator(
            IItemGeneratorFacade itemGeneratorFacade,
            IGeneratorContextFactory generatorContextFactory)
        {
            _itemGeneratorFacade = itemGeneratorFacade;
            _generatorContextFactory = generatorContextFactory;
        }

        public Type DropTableType { get; } = typeof(ItemDropTable);

        public IEnumerable<IGameObject> GenerateLoot(
            IDropTable dropTable,
            IGeneratorContext generatorContext)
        {
            Contract.Requires(
                dropTable.GetType() == DropTableType,
                $"The provided drop table '{dropTable}' must have the type '{DropTableType}'.");
            return GenerateLoot((IItemDropTable)dropTable, generatorContext);
        }

        private IEnumerable<IGameObject> GenerateLoot(
            IItemDropTable dropTable,
            IGeneratorContext generatorContext)
        {
            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the drop table.
            var currentDropContext = _generatorContextFactory.CreateGeneratorContext(
                dropTable.MinimumGenerateCount,
                dropTable.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(dropTable.ProvidedAttributes));

            // delegate generation of this table to someone else
            var generated = _itemGeneratorFacade.GenerateItems(currentDropContext);
            return generated;
        }
    }
}