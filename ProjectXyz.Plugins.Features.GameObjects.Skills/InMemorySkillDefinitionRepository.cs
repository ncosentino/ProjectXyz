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
                    new StringIdentifier("skill-definition-id"),
                    new IdentifierFilterAttributeValue(skillDefinitionId),
                    true));
            var enchantments = _enchantmentLoader.Load(enchantmentsFilterContext);
            return enchantments;
        }
    }
}
