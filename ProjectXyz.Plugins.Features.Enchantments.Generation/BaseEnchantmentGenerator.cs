using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IGeneratorComponentToBehaviorConverter _generatorComponentToBehaviorConverter;

        public BaseEnchantmentGenerator(
            IEnchantmentFactory enchantmentFactory,
            IRandomNumberGenerator randomNumberGenerator,
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IGeneratorComponentToBehaviorConverter generatorComponentToBehaviorConverter)
        {
            _enchantmentFactory = enchantmentFactory;
            _randomNumberGenerator = randomNumberGenerator;
            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _generatorComponentToBehaviorConverter = generatorComponentToBehaviorConverter;
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext)
        {
            var count = GetCount(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount);

            for (var i = 0; i < count; i++)
            {
                // pick the random enchantment definition that meets the context conditions
                var enchantmentDefinitionCandidates = _enchantmentDefinitionRepository.LoadEnchantmentDefinitions(generatorContext);
                var enchantmentDefinition = enchantmentDefinitionCandidates.RandomOrDefault(_randomNumberGenerator);
                if (enchantmentDefinition == null)
                {
                    throw new InvalidOperationException("Could not generate an enchantment for the provided context.");
                }

                // create the whole set of components for the enchantment from the enchantment generation components
                var enchantmentBehaviors = enchantmentDefinition
                    .GeneratorComponents
                    .SelectMany(_generatorComponentToBehaviorConverter.Convert);

                var enchantment = _enchantmentFactory.Create(enchantmentBehaviors);
                yield return enchantment;
            }
        }

        private int GetCount(
            int enchantmentCountMinimum,
            int enchantmentCountMaximum)
        {
            var count = _randomNumberGenerator.NextInRange(
                enchantmentCountMinimum,
                enchantmentCountMaximum);
            return count;
        }
    }
}