using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyBehavior : IBehavior
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }

        IIdentifier SkillSynergyDefinitionId { get; }

        IReadOnlyCollection<IIdentifier> SynergySkillIds { get; }
    }
}
