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
        private readonly IReadOnlyCollection<IEnchantmentDefinitionRepository> _enchantmentDefinitionRepositories;
        private readonly IGeneratorComponentToBehaviorConverter _generatorComponentToBehaviorConverter;

        public BaseEnchantmentGenerator(
            IEnchantmentFactory enchantmentFactory,
            IRandom random,
            IEnumerable<IEnchantmentDefinitionRepository> enchantmentDefinitionRepositories,
            IGeneratorComponentToBehaviorConverter generatorComponentToBehaviorConverter)
        {
            _enchantmentFactory = enchantmentFactory;
            _random = random;
            _enchantmentDefinitionRepositories = enchantmentDefinitionRepositories.ToArray();
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext)
        {
            var targetCount = GetCount(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount);

            var elligibleRepositories = new HashSet<IEnchantmentDefinitionRepository>(_enchantmentDefinitionRepositories);
            var currentCount = 0;
            while (currentCount < targetCount)
            {
                if (elligibleRepositories.Count < 1)
                {
                    throw new InvalidOperationException(
                        "Could not find elligible enchantment repositories " +
                        "with the provided context. Investigate the conditions " +
                        "on the context along with the available repositories.");
                }

                // pick the random enchantment definition that meets the context conditions
                var enchantmentDefinitionRepository = elligibleRepositories.RandomOrDefault(_random);
                var enchantmentDefinitionCandidates = enchantmentDefinitionRepository.ReadEnchantmentDefinitions(generatorContext);
                var enchantmentDefinition = enchantmentDefinitionCandidates.RandomOrDefault(_random);
                if (enchantmentDefinition == null)
                {
                    elligibleRepositories.Remove(enchantmentDefinitionRepository);
                    continue;
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