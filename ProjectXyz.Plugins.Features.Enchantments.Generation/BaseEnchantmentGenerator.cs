using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework.Collections;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation
{
    public sealed class BaseEnchantmentGenerator : IBaseEnchantmentGenerator
    {
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IRandom _random;
        private readonly IReadOnlyEnchantmentDefinitionRepositoryFacade _enchantmentDefinitionRepository;
        private readonly IFilterComponentToBehaviorConverter _filterComponentToBehaviorConverter;

        public BaseEnchantmentGenerator(
            IEnchantmentFactory enchantmentFactory,
            IRandom random,
            IReadOnlyEnchantmentDefinitionRepositoryFacade enchantmentDefinitionRepository,
            IFilterComponentToBehaviorConverter filterComponentToBehaviorConverter)
        {
            _enchantmentFactory = enchantmentFactory;
            _random = random;
            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _filterComponentToBehaviorConverter = filterComponentToBehaviorConverter;
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IFilterContext filterContext)
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
                var enchantmentBehaviors = enchantmentDefinition
                    .FilterComponents
                    .SelectMany(_filterComponentToBehaviorConverter.Convert);

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
                enchantmentCountMaximum + 1);
            return count;
        }
    }
}