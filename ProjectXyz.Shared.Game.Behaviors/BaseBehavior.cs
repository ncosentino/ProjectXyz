using ProjectXyz.Api.Behaviors;

using NexusLabs.Contracts;

namespace ProjectXyz.Shared.Game.Behaviors
{
    public abstract class BaseBehavior : IBehavior
    {
        public IHasBehaviors Owner { get; private set; }

        public void RegisteringToOwner(IHasBehaviors owner) => OnRegisteringToOwner(owner);

        public void RegisteredToOwner(IHasBehaviors owner) => OnRegisteredToOwner(owner);

        protected virtual void OnRegisteringToOwner(IHasBehaviors owner)
        {
            Owner = owner;
        }

        protected virtual void OnRegisteredToOwner(IHasBehaviors owner)
        {
            Contract.Requires(
                Owner == owner,
                $"Expecting Owner ({Owner}) == {nameof(owner)} ({owner}).");
        }
    }
}