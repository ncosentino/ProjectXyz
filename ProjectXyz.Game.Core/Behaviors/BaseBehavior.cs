using System;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public abstract class BaseBehavior : IBehavior
    {
        public IHasBehaviors Owner { get; private set; }

        public void RegisterTo(IHasBehaviors owner) => OnRegisterTo(owner);

        protected virtual void OnRegisterTo(IHasBehaviors owner)
        {
            if (Owner != null && Owner != owner)
            {
                throw new InvalidOperationException("An owner is already assigned.");
            }

            Owner = owner;
        }
    }
}