using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Standard;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item
{
    public sealed class ItemDropTableHandlerGenerator : IDiscoverableDropTableHandlerGenerator
    {
        private readonly IItemGeneratorFacade _itemGeneratorFacade;
        private readonly IFilterContextFactory _filterContextFactory;

        public ItemDropTableHandlerGenerator(
            IItemGeneratorFacade itemGeneratorFacade,
            IFilterContextFactory filterContextFactory)
        {
            _itemGeneratorFacade = itemGeneratorFacade;
            _filterContextFactory = filterContextFactory;
        }

        public Type DropTableType { get; } = typeof(ItemDropTable);

        public IEnumerable<IGameObject> GenerateLoot(
            IDropTable dropTable,
            IFilterContext filterContext)
        {
            Contract.Requires(
                () => dropTable.GetType() == DropTableType,
                () => $"The provided drop table '{dropTable}' must have the type '{DropTableType}'.");
            return GenerateLoot((IItemDropTable)dropTable, filterContext);
        }

        private IEnumerable<IGameObject> GenerateLoot(
            IItemDropTable dropTable,
            IFilterContext filterContext)
        {
            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the drop table.
            var dropTableProvidedAttributes = dropTable.ProvidedAttributes.ToDictionary(
                x => x.Id,
                x => x);
            var currentDropContext = _filterContextFactory.CreateContext(
                dropTable.MinimumGenerateCount,
                dropTable.MaximumGenerateCount,
                filterContext
                    .Attributes
                    .Where(x => !dropTableProvidedAttributes.ContainsKey(x.Id))
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