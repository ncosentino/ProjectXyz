using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class Actor : IGameObject
    {
        public Actor(IBehaviorCollection behaviors)
        {
            Behaviors = behaviors;
        }

        public IBehaviorCollection Behaviors { get; }
    }
}