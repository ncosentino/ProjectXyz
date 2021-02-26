using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class InMemorySkillDefinitionRepository : IDiscoverableSkillDefinitionRepository
    {
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IReadOnlyCollection<ISkillDefinition> _skillDefinitions;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IEnchantmentLoader _enchantmentLoader;

        public InMemorySkillDefinitionRepository(
            IAttributeFilterer attributeFilterer,
            IEnumerable<ISkillDefinition> skillDefinitions,
            IFilterContextFactory filterContextFactory,
            IEnchantmentLoader enchantmentLoader)
        {
            _attributeFilterer = attributeFilterer;
            _skillDefinitions = skillDefinitions.ToArray();
            _filterContextFactory = filterContextFactory;
            _enchantmentLoader = enchantmentLoader;
        }

        /// <inheritdoc/>
        public IEnumerable<ISkillDefinition> GetSkillDefinitions(IFilterContext filterContext)
        {
            var filtered = _attributeFilterer.Filter(
                _skillDefinitions,
                filterContext);
            return filtered;
        }

        /// <inheritdoc/>
        public IEnumerable<IEnchantment> GetSkillDefinitionStatefulEnchantments(IIdentifier skillDefinitionId)
        {
            var enchantmentsFilterContext = _filterContextFactory
                .CreateFilterContextForAnyAmount(new FilterAttribute(
                    // FIXME: should we be consistent and request enchantments 
                    // keyed by THEIR ID, and not inverting it by putting the 
                    // skill definition ID into it? Following this current 
                    // pattern it means you bleed N number of
                    // enchantment-wanting-things into your enchantment 
                    // definitions. Inverting this means N number of things can
                    // KNOW about enchantment definitions though, which feels
                    // better? if so, this means skill definitions need
                    // enchantment definition ID's on them. I assume I coded it
                    // this way originally because it easily handles multiple
                    // enchantments (i.e. skill definitions will need a
                    // COLLECTION of enchantment definition IDs)
                    new StringIdentifier("skill-definition-id"),
                    new IdentifierFilterAttributeValue(skillDefinitionId),
                    true));
            var enchantments = _enchantmentLoader.Load(enchantmentsFilterContext);
            return enchantments;
        }
    }
}
