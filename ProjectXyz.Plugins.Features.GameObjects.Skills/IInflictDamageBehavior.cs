using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IInflictDamageBehavior : IBehavior
    {
    }

    public sealed class InflictDamageBehavior : BaseBehavior, IInflictDamageBehavior
    {
    }
}
