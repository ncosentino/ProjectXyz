using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
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

        public IEnumerable<IGameObject> GenerateLoot(IFilterContext filterContext)
        {
            // filter the drop tables
            var allDropTables = _dropTableRepository.GetAllDropTables();
            var filteredDropTables = _attributeFilterer
                .Filter(
                    allDropTables,
                    filterContext)
                .ToArray();
            if (filteredDropTables.Length < 1)
            {
                if (filterContext.MinimumCount < 1)
                {
                    yield break;
                }

                throw new InvalidOperationException(
                    $"There was no drop table that could be selected from " +
                    $"the set of filtered drop tables using context '{filterContext}'.");
            }

            Contract.Requires(
                filterContext.MinimumCount <= filterContext.MaximumCount,
                $"The generation context must have a maximum " +
                $"({filterContext.MaximumCount}) greater than or " +
                $"equal to the minimum ({filterContext.MinimumCount}).");
            Contract.Requires(
                filterContext.MinimumCount >= 0,
                $"The generation context must have a minimum " +
                $"({filterContext.MinimumCount}) greater than or " +
                $"equal to zero.");

            var targetCount = GetGenerationCount(
                filterContext.MinimumCount,
                filterContext.MaximumCount);
            var dropTableCandidates = new HashSet<IDropTable>(filteredDropTables);
            for (var generationCount = 0; generationCount < targetCount; /* no increment here */)
            {
                // random roll the drop table
                var dropTable = dropTableCandidates.RandomOrDefault(_random);
                if (dropTable == null)
                {
                    if (generationCount >= filterContext.MinimumCount)
                    {
                        yield break;
                    }

                    throw new InvalidOperationException(
                        $"Randomized selection of drop tables failed to select " +
                        $"a valid drop table. Are any in the enumerable set?");
                }

                var generationCountBeforeDrop = generationCount;
                var generatedLoot = _dropTableHandlerGeneratorFacade.GenerateLoot(
                    dropTable,
                    filterContext);
                foreach (var loot in generatedLoot)
                {
                    if (generationCount >= targetCount)
                    {
                        break;
                    }

                    yield return loot;
                    generationCount++;
                }

                // if this drop table didn't yield any items, we can forget 
                // about it on future attempts with this context
                if (generationCount == generationCountBeforeDrop)
                {
                    dropTableCandidates.Remove(dropTable);
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