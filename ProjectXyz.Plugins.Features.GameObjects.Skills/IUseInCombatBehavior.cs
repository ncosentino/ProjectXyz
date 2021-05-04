using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IUseInCombatBehavior : IBehavior
    {
    }

    public sealed class UseInCombatBehavior : BaseBehavior, IUseInCombatBehavior
    {
    }
}
