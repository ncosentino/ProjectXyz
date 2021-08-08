using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default
{
    public sealed class ReplaceIngredientsCraftingHandler : IDiscoverableCraftingHandler
    {
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IReplaceIngredientsCraftingRepositoryFacade _replaceIngredientsCraftingRepository;
        private readonly ILootGenerator _lootGenerator;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IDropTableIdentifiers _dropTableIdentifiers;
        private readonly IDropTableRepositoryFacade _dropTableRepository;

        public ReplaceIngredientsCraftingHandler(
            IAttributeFilterer attributeFilterer,
            IReplaceIngredientsCraftingRepositoryFacade replaceIngredientsCraftingRepository,
            ILootGenerator lootGenerator,
            IFilterContextFactory filterContextFactory,
            IDropTableIdentifiers dropTableIdentifiers,
            IDropTableRepositoryFacade dropTableRepository)
        {
            _attributeFilterer = attributeFilterer;
            _replaceIngredientsCraftingRepository = replaceIngredientsCraftingRepository;
            _lootGenerator = lootGenerator;
            _filterContextFactory = filterContextFactory;
            _dropTableIdentifiers = dropTableIdentifiers;
            _dropTableRepository = dropTableRepository;
        }

        public bool TryHandle(
            IReadOnlyCollection<IFilterAttribute> filterAttributes,
            IReadOnlyCollection<IGameObject> ingredients,
            out IReadOnlyCollection<IGameObject> newItems)
        {
            var filteredDefinitions = _attributeFilterer.BidirectionalFilter(
                _replaceIngredientsCraftingRepository.GetAll(),
                filterAttributes);

            IReadOnlyCollection<IGameObject> resultingItems = null;
            Parallel.ForEach(filteredDefinitions, (definition, filterDefinitionsLoopState, _) =>
            {
                if (definition.IngredientFilters.Count != ingredients.Count)
                {
                    return;
                }

                // FIXME: this logic doesn't handle if one ingredient can
                // handle multiple filter conditions and we "count" it towards
                // one that coud have been met by another item, thus forfeiting
                // it for consideration on a still remaining filter
                var remainingFilters = new HashSet<IReadOnlyCollection<IFilterAttributeValue>>(definition.IngredientFilters);
                foreach (var ingredient in ingredients)
                {
                    if (filterDefinitionsLoopState.IsStopped)
                    {
                        return;
                    }

                    var match = remainingFilters.FirstOrDefault(ingredientFilters => _attributeFilterer.IsMatch(ingredient, ingredientFilters));
                    if (match == null)
                    {
                        return;
                    }

                    remainingFilters.Remove(match);
                }

                if (!filterDefinitionsLoopState.IsStopped)
                {
                    filterDefinitionsLoopState.Stop();
                    resultingItems = GenerateResultingItems(
                        filterAttributes,
                        definition);
                }
            });

            newItems = resultingItems;
            return resultingItems != null;
        }

        private IReadOnlyCollection<IGameObject> GenerateResultingItems(
            IReadOnlyCollection<IFilterAttribute> filterAttributes,
            IReplaceIngredientsCraftingDefinition definition)
        {
            var itemAccumulator = new List<IGameObject>();
            foreach (var dropTableId in definition.DropTableIds)
            {
                var dropTable = _dropTableRepository.GetForDropTableId(dropTableId);

                var filterContext = _filterContextFactory.CreateContext(
                    dropTable.MinimumGenerateCount,
                    dropTable.MaximumGenerateCount,
                    new[]
                    {
                        new FilterAttribute(
                            _dropTableIdentifiers.FilterContextDropTableIdentifier,
                            new IdentifierFilterAttributeValue(dropTableId),
                            true),
                    }
                    .Concat(filterAttributes));
                itemAccumulator.AddRange(_lootGenerator.GenerateLoot(filterContext));
            }

            return itemAccumulator;
        }
    }
}
