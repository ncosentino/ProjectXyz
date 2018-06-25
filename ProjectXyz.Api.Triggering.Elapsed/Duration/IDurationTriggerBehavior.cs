using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Api.Duration
{
    public interface IDurationTriggerBehavior : IBehavior
    {
        IInterval Duration { get; }
    }
}