using System;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Framework.Contracts;

namespace ProjectXyz.Game.Core.Behaviors
{
    public abstract class BaseBehavior : IBehavior
    {
        public IHasBehaviors Owner { get; private set; }

        public void RegisteringToOwner(IHasBehaviors owner) => OnRegisteringToOwner(owner);

        public void RegisteredToOwner(IHasBehaviors owner) => OnRegisteredToOwner(owner);

        protected virtual void OnRegisteringToOwner(IHasBehaviors owner)
        {
            if (Owner != null && Owner != owner)
            {
                throw new InvalidOperationException("An owner is already assigned.");
            }

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