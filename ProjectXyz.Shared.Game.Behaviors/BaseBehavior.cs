using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Shared.Game.Behaviors
{
    public abstract class BaseBehavior : IRegisterableBehavior
    {
        public IGameObject Owner { get; private set; }

        public void RegisteringToOwner(IGameObject owner) => OnRegisteringToOwner(owner);

        public void RegisteredToOwner(IGameObject owner) => OnRegisteredToOwner(owner);

        protected virtual void OnRegisteringToOwner(IGameObject owner)
        {
            Owner = owner;
        }

        protected virtual void OnRegisteredToOwner(IGameObject owner)
        {
            Contract.Requires(
                () => Owner == owner,
                () => $"Expecting {nameof(Owner)} ({Owner}) == {nameof(owner)} ({owner}).");
        }
    }
}