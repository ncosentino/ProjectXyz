using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public sealed class SkillSynergyDefinition : ISkillSynergyDefinition
    {
        public IIdentifier Id { get; }

        public IReadOnlyCollection<IFilterAttribute> SkillDefinitionFilters { get; }

        public IReadOnlyCollection<IFilterAttribute> EnchantmentDefinitionFilters { get; }
    }
}
