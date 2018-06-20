using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IDurationTriggerBehavior : IBehavior
    {
        IInterval Duration { get; }
    }
}