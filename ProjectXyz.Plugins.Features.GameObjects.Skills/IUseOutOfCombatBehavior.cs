using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IUseOutOfCombatBehavior : IBehavior
    {
    }

    public sealed class UseOutOfCombatBehavior : BaseBehavior, IUseOutOfCombatBehavior
    {
    }
}
