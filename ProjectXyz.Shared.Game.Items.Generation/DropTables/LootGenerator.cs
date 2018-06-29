using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class LootGenerator : ILootGenerator
    {
        private readonly IDropTableRepository _dropTableRepository;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IDropTableHandlerGeneratorFacade _dropTableHandlerGeneratorFacade;

        public LootGenerator(
            IDropTableRepository dropTableRepository,
            IDropTableHandlerGeneratorFacade dropTableHandlerGeneratorFacade,
            IAttributeFilterer attributeFilterer,
            IRandomNumberGenerator randomNumberGenerator)
        {
            _dropTableRepository = dropTableRepository;
            _dropTableHandlerGeneratorFacade = dropTableHandlerGeneratorFacade;
            _attributeFilterer = attributeFilterer;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public IEnumerable<IGameObject> GenerateLoot(IGeneratorContext generatorContext)
        {
            // filter the drop tables
            var allDropTables = _dropTableRepository.GetAllDropTables();
            var filteredDropTables = _attributeFilterer.Filter(
                allDropTables,
                generatorContext);

            int generatedCount = 0;
            int remaining = generatorContext.MinimumGenerateCount;
            while (remaining > 0 && generatedCount < generatorContext.MaximumGenerateCount)
            {
                // random roll the drop table
                var dropTable = filteredDropTables.RandomOrDefault(_randomNumberGenerator);
                if (dropTable == null)
                {
                    throw new InvalidOperationException(
                        $"There was no drop table that could be selected from " +
                        $"the set of filtered drop tables using context '{generatorContext}'.");
                }

                var generatedLoot = _dropTableHandlerGeneratorFacade.GenerateLoot(
                    dropTable,
                    generatorContext);
                foreach (var loot in generatedLoot)
                {
                    if (remaining == 0 || generatedCount == generatorContext.MaximumGenerateCount)
                    {
                        break;
                    }

                    yield return loot;
                    remaining -= 1;
                    generatedCount += 1;
                }
            }
        }
    }
}