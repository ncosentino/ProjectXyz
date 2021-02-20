
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillTargetModeBehavior : IBehavior
    {
        IIdentifier TargetModeId { get; }
    }
}
