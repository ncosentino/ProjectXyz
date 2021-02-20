using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public sealed class SkillSynergyBehavior :
        BaseBehavior,
        ISkillSynergyBehavior
    {
        public SkillSynergyBehavior(
            IIdentifier skillSynergyDefinitionId,
            IEnumerable<IIdentifier> synergySkillIds,
            IEnumerable<IEnchantment> enchantments)
        {
            SkillSynergyDefinitionId = skillSynergyDefinitionId;
            SynergySkillIds = synergySkillIds.ToArray();
            Enchantments = enchantments.ToArray();
        }

        public IIdentifier SkillSynergyDefinitionId { get; }
        public IReadOnlyCollection<IIdentifier> SynergySkillIds { get; }
        public IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}
