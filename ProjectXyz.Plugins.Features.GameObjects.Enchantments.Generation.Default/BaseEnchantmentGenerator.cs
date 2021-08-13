using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.Default
{
    public sealed class BaseEnchantmentGenerator : IBaseEnchantmentGenerator
    {
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IRandom _random;
        private readonly IReadOnlyEnchantmentDefinitionRepositoryFacade _enchantmentDefinitionRepository;
        private readonly IGeneratorComponentToBehaviorConverterFacade _filterComponentToBehaviorConverter;

        public BaseEnchantmentGenerator(
            IEnchantmentFactory enchantmentFactory,
            IRandom random,
            IReadOnlyEnchantmentDefinitionRepositoryFacade enchantmentDefinitionRepository,
            IGeneratorComponentToBehaviorConverterFacade filterComponentToBehaviorConverter)
        {
            _enchantmentFactory = enchantmentFactory;
            _random = random;
            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _filterComponentToBehaviorConverter = filterComponentToBehaviorConverter;
        }

        public IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext)
        {
            var targetCount = GetCount(
                filterContext.MinimumCount,
                filterContext.MaximumCount);

            var currentCount = 0;
            while (currentCount < targetCount)
            {
                // pick the random enchantment definition that meets the context conditions
                var enchantmentDefinitionCandidates = _enchantmentDefinitionRepository.ReadEnchantmentDefinitions(filterContext);
                var enchantmentDefinition = enchantmentDefinitionCandidates.RandomOrDefault(_random);
                if (enchantmentDefinition == null)
                {
                    throw new InvalidOperationException(
                        "Could not read enchantment definitions that meet the " +
                        "required context. Inspect the context provided and " +
                        "ensure that there are repositories configured that " +
                        "meet the criteria.");
                }

                // create the whole set of components for the enchantment from the enchantment generation components
                var enchantmentBehaviors = _filterComponentToBehaviorConverter.Convert(
                    filterContext,
                    Enumerable.Empty<IBehavior>(),
                    enchantmentDefinition.GeneratorComponents);
                var enchantment = _enchantmentFactory.Create(enchantmentBehaviors);
                yield return enchantment;
                currentCount++;
            }
        }

        private int GetCount(
            int enchantmentCountMinimum,
            int enchantmentCountMaximum)
        {
            var count = _random.Next(
                enchantmentCountMinimum,
                enchantmentCountMaximum == int.MaxValue
                    ? int.MaxValue
                    : enchantmentCountMaximum + 1);
            return count;
        }
    }
}