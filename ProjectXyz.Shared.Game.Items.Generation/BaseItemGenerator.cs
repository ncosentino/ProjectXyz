using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class BaseItemGenerator : IBaseItemGenerator
    {
        private readonly IItemFactory _itemFactory;
        private readonly IRandom _random;
        private readonly IItemDefinitionRepositoryFacade _itemDefinitionRepository;
        private readonly IFilterComponentToBehaviorConverter _filterComponentToBehaviorConverter;

        public BaseItemGenerator(
            IItemFactory itemFactory,
            IRandom random,
            IItemDefinitionRepositoryFacade itemDefinitionRepository,
            IFilterComponentToBehaviorConverter filterComponentToBehaviorConverter)
        {
            _itemFactory = itemFactory;
            _random = random;
            _itemDefinitionRepository = itemDefinitionRepository;
            _filterComponentToBehaviorConverter = filterComponentToBehaviorConverter;
        }

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            var count = GetCount(
                filterContext.MinimumCount,
                filterContext.MaximumCount);

            for (var i = 0; i < count; i++)
            {
                // pick the random item definition that meets the context conditions
                var itemDefinitionCandidates = _itemDefinitionRepository.LoadItemDefinitions(filterContext);
                var itemDefinition = itemDefinitionCandidates.RandomOrDefault(_random);
                if (itemDefinition == null)
                {
                    throw new InvalidOperationException("Could not generate an item for the provided context.");
                }

                // create the whole set of components for the item from the item generation components
                var itemBehaviors = itemDefinition
                    .FilterComponents
                    .SelectMany(_filterComponentToBehaviorConverter.Convert);

                var item = _itemFactory.Create(itemBehaviors);
                yield return item;
            }
        }

        private int GetCount(
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