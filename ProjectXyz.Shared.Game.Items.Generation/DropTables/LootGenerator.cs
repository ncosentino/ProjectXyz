using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class LootGenerator : ILootGenerator
    {
        private readonly IDropTableRepositoryFacade _dropTableRepository;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IRandom _random;
        private readonly IDropTableHandlerGeneratorFacade _dropTableHandlerGeneratorFacade;

        public LootGenerator(
            IDropTableRepositoryFacade dropTableRepository,
            IDropTableHandlerGeneratorFacade dropTableHandlerGeneratorFacade,
            IAttributeFilterer attributeFilterer,
            IRandom random)
        {
            _dropTableRepository = dropTableRepository;
            _dropTableHandlerGeneratorFacade = dropTableHandlerGeneratorFacade;
            _attributeFilterer = attributeFilterer;
            _random = random;
        }

        public IEnumerable<IGameObject> GenerateLoot(IGeneratorContext generatorContext)
        {
            // filter the drop tables
            var allDropTables = _dropTableRepository.GetAllDropTables();
            var filteredDropTables = _attributeFilterer
                .Filter(
                    allDropTables,
                    generatorContext)
                .ToArray();
            if (filteredDropTables.Length < 1)
            {
                if (generatorContext.MinimumGenerateCount < 1)
                {
                    yield break;
                }

                throw new InvalidOperationException(
                    $"There was no drop table that could be selected from " +
                    $"the set of filtered drop tables using context '{generatorContext}'.");
            }

            Contract.Requires(
                generatorContext.MinimumGenerateCount <= generatorContext.MaximumGenerateCount,
                $"The generation context must have a maximum " +
                $"({generatorContext.MaximumGenerateCount}) greater than or " +
                $"equal to the minimum ({generatorContext.MinimumGenerateCount}).");
            Contract.Requires(
                generatorContext.MinimumGenerateCount >= 0,
                $"The generation context must have a minimum " +
                $"({generatorContext.MinimumGenerateCount}) greater than or " +
                $"equal to zero.");

            var targetCount = GetGenerationCount(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount);
            for (var generationIndex = 0; generationIndex < targetCount; /* no increment here */)
            {
                // random roll the drop table
                var dropTable = filteredDropTables.RandomOrDefault(_random);
                if (dropTable == null)
                {
                    throw new InvalidOperationException(
                        $"Randomized selection of drop tables failed to select " +
                        $"a valid drop table. Are any in the enumerable set?");
                }

                var generatedLoot = _dropTableHandlerGeneratorFacade.GenerateLoot(
                    dropTable,
                    generatorContext);
                foreach (var loot in generatedLoot)
                {
                    if (generationIndex >= targetCount)
                    {
                        break;
                    }

                    yield return loot;
                    generationIndex++;
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
    }
}