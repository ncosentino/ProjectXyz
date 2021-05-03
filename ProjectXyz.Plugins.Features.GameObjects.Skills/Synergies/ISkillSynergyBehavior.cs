using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> Enchantments { get; }

        IIdentifier SkillSynergyDefinitionId { get; }

        IReadOnlyCollection<IIdentifier> SynergySkillIds { get; }
    }
}
