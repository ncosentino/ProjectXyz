using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IBehaviorManager
    {
        void Register(
            IHasBehaviors owner,
            IBehaviorCollection behaviors);
    }
}