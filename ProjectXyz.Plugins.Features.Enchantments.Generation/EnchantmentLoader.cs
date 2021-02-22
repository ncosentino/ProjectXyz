using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation
{
    public sealed class EnchantmentLoader : IEnchantmentLoader
    {
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IReadOnlyEnchantmentDefinitionRepositoryFacade _enchantmentDefinitionRepository;
        private readonly IFilterComponentToBehaviorConverter _filterComponentToBehaviorConverter;

        public EnchantmentLoader(
            IEnchantmentFactory enchantmentFactory,
            IReadOnlyEnchantmentDefinitionRepositoryFacade enchantmentDefinitionRepository,
            IFilterComponentToBehaviorConverter filterComponentToBehaviorConverter)
        {
            _enchantmentFactory = enchantmentFactory;
            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _filterComponentToBehaviorConverter = filterComponentToBehaviorConverter;
        }

        public IEnumerable<IEnchantment> Load(IFilterContext filterContext)
        {
            foreach (var enchantmentDefinition in _enchantmentDefinitionRepository
                .ReadEnchantmentDefinitions(filterContext))
            {
                var enchantmentBehaviors = enchantmentDefinition
                    .FilterComponents
                    .SelectMany(_filterComponentToBehaviorConverter.Convert);
                var enchantment = _enchantmentFactory.Create(enchantmentBehaviors);
                yield return enchantment;
            }
        }
    }
}