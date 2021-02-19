using System.Collections.Generic;

namespace ProjectXyz.Api.Behaviors
{
    public interface IHasBehaviors
    {
        IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
