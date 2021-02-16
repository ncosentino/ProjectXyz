using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class BaseItemGenerator : IBaseItemGenerator
    {
        private readonly IItemFactory _itemFactory;
        private readonly IRandom _random;
        private readonly IItemDefinitionRepositoryFacade _itemDefinitionRepository;
        private readonly IGeneratorComponentToBehaviorConverter _generatorComponentToBehaviorConverter;

        public BaseItemGenerator(
            IItemFactory itemFactory,
            IRandom random,
            IItemDefinitionRepositoryFacade itemDefinitionRepository,
            IGeneratorComponentToBehaviorConverter generatorComponentToBehaviorConverter)
        {
            _itemFactory = itemFactory;
            _random = random;
            _itemDefinitionRepository = itemDefinitionRepository;
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var count = GetCount(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount);

            for (var i = 0; i < count; i++)
            {
                // pick the random item definition that meets the context conditions
                var itemDefinitionCandidates = _itemDefinitionRepository.LoadItemDefinitions(generatorContext);
                var itemDefinition = itemDefinitionCandidates.RandomOrDefault(_random);
                if (itemDefinition == null)
                {
                    throw new InvalidOperationException("Could not generate an item for the provided context.");
                }

                // create the whole set of components for the item from the item generation components
                var itemBehaviors = itemDefinition
                    .GeneratorComponents
                    .SelectMany(_generatorComponentToBehaviorConverter.Convert);

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