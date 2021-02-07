using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation
{
    public sealed class BaseEnchantmentGenerator : IBaseEnchantmentGenerator
    {
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IRandom _random;
        private readonly IReadOnlyEnchantmentDefinitionRepositoryFacade _enchantmentDefinitionRepository;
        private readonly IGeneratorComponentToBehaviorConverter _generatorComponentToBehaviorConverter;

        public BaseEnchantmentGenerator(
            IEnchantmentFactory enchantmentFactory,
            IRandom random,
            IReadOnlyEnchantmentDefinitionRepositoryFacade enchantmentDefinitionRepository,
            IGeneratorComponentToBehaviorConverter generatorComponentToBehaviorConverter)
        {
            _enchantmentFactory = enchantmentFactory;
            _random = random;
            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext)
        {
            var targetCount = GetCount(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount);

            var currentCount = 0;
            while (currentCount < targetCount)
            {
                // pick the random enchantment definition that meets the context conditions
                var enchantmentDefinitionCandidates = _enchantmentDefinitionRepository.ReadEnchantmentDefinitions(generatorContext);
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
                    .GeneratorComponents
                    .SelectMany(_generatorComponentToBehaviorConverter.Convert);

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