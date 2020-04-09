using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

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
            // Create a new context
            var dropTableRequiredAttributeIds = new HashSet<IIdentifier>(dropTable
                .SupportedAttributes
                .Where(x => x.Required)
                .Select(x => x.Id));
            var currentDropContext = _generatorContextFactory.CreateGeneratorContext(
                dropTable.MinimumGenerateCount,
                dropTable.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Where(x => !dropTableRequiredAttributeIds.Contains(x.Id))
                    .Concat(dropTable.ProvidedAttributes));

            // delegate generation of this table to someone else
            var generated = _itemGeneratorFacade.GenerateItems(currentDropContext);
            return generated;
        }
    }
}