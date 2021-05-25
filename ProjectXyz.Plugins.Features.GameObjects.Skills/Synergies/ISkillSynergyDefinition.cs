using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyDefinition
    {
        IReadOnlyCollection<IFilterAttribute> EnchantmentDefinitionFilters { get; }
        IIdentifier Id { get; }
        IReadOnlyCollection<IFilterAttribute> SkillDefinitionFilters { get; }
    }
}
