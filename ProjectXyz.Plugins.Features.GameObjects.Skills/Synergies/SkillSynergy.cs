using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public sealed class SkillSynergy : IGameObject
    {
        public SkillSynergy(
            SkillSynergyDefinitionIdBehavior skillSynergyDefinitionIdBehavior,
            SkillSynergyEffectedSkillsBehavior skillSynergyEffectedSkillsBehavior,
            IHasReadOnlyEnchantmentsBehavior hasEnchantmentsBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            Behaviors = new IBehavior[]
            {
                skillSynergyDefinitionIdBehavior,
                skillSynergyEffectedSkillsBehavior,
                hasEnchantmentsBehavior
            }
            .Concat(additionalBehaviors)
            .ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
