using System.Collections.Generic;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasBehaviors
    {
        IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
