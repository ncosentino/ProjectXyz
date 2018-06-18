using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Framework.Extensions;
using ProjectXyz.Framework.Extensions.Collections;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Shared.Game.Items.Generation
{
    public sealed class BaseItemGenerator : IBaseItemGenerator
    {
        private readonly IItemFactory _itemFactory;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IItemDefinitionRepository _itemDefinitionRepository;
        private readonly IItemGeneratorComponentToBehaviorConverter _itemGeneratorComponentToBehaviorConverter;

        public BaseItemGenerator(
            IItemFactory itemFactory,
            IRandomNumberGenerator randomNumberGenerator,
            IItemDefinitionRepository itemDefinitionRepository,
            IItemGeneratorComponentToBehaviorConverter itemGeneratorComponentToBehaviorConverter)
        {
            _itemFactory = itemFactory;
            _randomNumberGenerator = randomNumberGenerator;
            _itemDefinitionRepository = itemDefinitionRepository;
            _itemGeneratorComponentToBehaviorConverter = itemGeneratorComponentToBehaviorConverter;
        }

        public IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext)
        {
            var count = GetCount(
                itemGeneratorContext.MinimumGenerateCount,
                itemGeneratorContext.MaximumGenerateCount);

            for (var i = 0; i < count; i++)
            {
                // pick the random item definition that meets the context conditions
                var itemDefinitionCandidates = _itemDefinitionRepository.LoadItemDefinitions(itemGeneratorContext);
                var itemDefinition = itemDefinitionCandidates.RandomOrDefault(new Random()); //.RandomOrDefault(_randomNumberGenerator);
                if (itemDefinition == null)
                {
                    throw new InvalidOperationException("Could not generate an item for the provided context.");
                }

                // create the whole set of components for the item from the item generation components
                var itemBehaviors = itemDefinition
                    .GeneratorComponents
                    .SelectMany(_itemGeneratorComponentToBehaviorConverter.Convert);
                
                var item = _itemFactory.Create(itemBehaviors);
                yield return item;
            }
        }

        private int GetCount(
            int itemCountMinimum,
            int itemCountMaximum)
        {
            var count = _randomNumberGenerator.NextInRange(
                itemCountMinimum,
                itemCountMaximum);
            return count;
        }
    }
}