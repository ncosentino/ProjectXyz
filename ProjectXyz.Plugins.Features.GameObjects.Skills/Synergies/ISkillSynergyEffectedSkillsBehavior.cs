using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyEffectedSkillsBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> SkillDefinitionIds { get; }
    }
}
