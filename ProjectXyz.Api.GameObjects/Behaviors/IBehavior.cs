using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.GameObjects.Behaviors
{
    public interface IBehavior
    {
        IGameObject Owner { get; }
    }
}