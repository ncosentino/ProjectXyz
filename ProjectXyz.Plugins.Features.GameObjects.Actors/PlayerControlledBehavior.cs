using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public class PlayerControlledBehavior :
        BaseBehavior,
        IPlayerControlledBehavior
    {
        public bool IsActive { get; set; }
    }
}
