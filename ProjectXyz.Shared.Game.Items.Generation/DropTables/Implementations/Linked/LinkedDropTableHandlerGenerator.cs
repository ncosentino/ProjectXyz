using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked
{
    public sealed class LinkedDropTableHandlerGenerator : IDiscoverableDropTableHandlerGenerator
    {
        private readonly IRandom _random;
        private readonly IDropTableHandlerGeneratorFacade _dropTableHandlerGeneratorFacade;
        private readonly IDropTableRepositoryFacade _dropTableRepository;
        private readonly IGeneratorContextFactory _generatorContextFactory;

        public LinkedDropTableHandlerGenerator(
            IRandom random,
            IDropTableHandlerGeneratorFacade dropTableHandlerGeneratorFacade,
            IDropTableRepositoryFacade dropTableRepository,
            IGeneratorContextFactory generatorContextFactory)
        {
            _random = random;
            _dropTableHandlerGeneratorFacade = dropTableHandlerGeneratorFacade;
            _dropTableRepository = dropTableRepository;
            _generatorContextFactory = generatorContextFactory;
        }

        public Type DropTableType { get; } = typeof(LinkedDropTable);

        public IEnumerable<IGameObject> GenerateLoot(
            IDropTable dropTable,
            IGeneratorContext generatorContext)
        {
            Contract.Requires(
                dropTable.GetType() == DropTableType,
                $"The provided drop table '{dropTable}' must have the type '{DropTableType}'.");
            return GenerateLoot((ILinkedDropTable)dropTable, generatorContext);
        }

        private IEnumerable<IGameObject> GenerateLoot(
            ILinkedDropTable dropTable,
            IGeneratorContext generatorContext)
        {
            // get a random count for this drop table
            var generationCount = _random.Next(
                dropTable.MinimumGenerateCount,
                dropTable.MaximumGenerateCount + 1);

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
                var linkedDropContext = _generatorContextFactory.CreateGeneratorContext(
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