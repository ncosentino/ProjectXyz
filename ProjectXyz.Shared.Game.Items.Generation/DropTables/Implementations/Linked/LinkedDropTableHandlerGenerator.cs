using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables.Linked;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked
{
    public sealed class LinkedDropTableHandlerGenerator : IDiscoverableDropTableHandlerGenerator
    {
        private readonly IRandom _random;
        private readonly IDropTableHandlerGeneratorFacade _dropTableHandlerGeneratorFacade;
        private readonly IDropTableRepositoryFacade _dropTableRepository;
        private readonly IFilterContextFactory _filterContextFactory;

        public LinkedDropTableHandlerGenerator(
            IRandom random,
            IDropTableHandlerGeneratorFacade dropTableHandlerGeneratorFacade,
            IDropTableRepositoryFacade dropTableRepository,
            IFilterContextFactory filterContextFactory)
        {
            _random = random;
            _dropTableHandlerGeneratorFacade = dropTableHandlerGeneratorFacade;
            _dropTableRepository = dropTableRepository;
            _filterContextFactory = filterContextFactory;
        }

        public Type DropTableType { get; } = typeof(LinkedDropTable);

        public IEnumerable<IGameObject> GenerateLoot(
            IDropTable dropTable,
            IFilterContext filterContext)
        {
            Contract.Requires(
                dropTable.GetType() == DropTableType,
                $"The provided drop table '{dropTable}' must have the type '{DropTableType}'.");
            return GenerateLoot((ILinkedDropTable)dropTable, filterContext);
        }

        private IEnumerable<IGameObject> GenerateLoot(
            ILinkedDropTable dropTable,
            IFilterContext filterContext)
        {
            // get a random count for this drop table
            var generationCount = GetGenerationCount(
                dropTable.MinimumGenerateCount,
                dropTable.MaximumGenerateCount);

            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the drop table.
            var currentDropContext = _filterContextFactory.CreateContext(
                dropTable.MinimumGenerateCount,
                dropTable.MaximumGenerateCount,
                filterContext
                    .Attributes
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(dropTable.ProvidedAttributes));

            // calculate the total weight once
            var totalWeight = dropTable.Entries.Sum(x => x.Weight);

            for (int i = 0; i < generationCount; i++)
            {
                // pick an entry
                var entry = PickWeightedEntry(
                    dropTable.Entries,
                    totalWeight);
                var linkedDropTableId = entry.DropTableId;

                // load the new drop table
                var linkedDropTable = _dropTableRepository.GetForDropTableId(linkedDropTableId);

                // Create a new context
                var linkedDropContext = _filterContextFactory.CreateContext(
                    linkedDropTable.MinimumGenerateCount,
                    linkedDropTable.MaximumGenerateCount,
                    currentDropContext.Attributes.Concat(linkedDropTable.ProvidedAttributes));

                // delegate generation of this table to someone else
                var generated = _dropTableHandlerGeneratorFacade.GenerateLoot(
                    linkedDropTable,
                    linkedDropContext);
                foreach (var gameObject in generated)
                {
                    yield return gameObject;
                }
            }
        }

        private int GetGenerationCount(
            int itemCountMinimum,
            int itemCountMaximum)
        {
            var count = _random.Next(
                itemCountMinimum,
                itemCountMaximum == int.MaxValue
                    ? int.MaxValue
                    : itemCountMaximum + 1);
            return count;
        }

        private IWeightedEntry PickWeightedEntry(
            IEnumerable<IWeightedEntry> entries,
            double totalWeight)
        {
            var randomNumber = _random.NextDouble(0, totalWeight);

            foreach (var entry in entries)
            {
                if (randomNumber <= entry.Weight)
                {
                    return entry;
                }

                randomNumber = randomNumber - entry.Weight;
            }

            throw new InvalidOperationException("No weighted entry was selected.");
        }
    }
}