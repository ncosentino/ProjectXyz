
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyDefinitionIdBehavior : IBehavior
    {
        IIdentifier SkillSynergyDefinitionId { get; }
    }
}
