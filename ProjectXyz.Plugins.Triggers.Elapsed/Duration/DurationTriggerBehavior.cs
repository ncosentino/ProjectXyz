using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Triggers.Elapsed.Duration
{
    public sealed class DurationTriggerBehavior :
        BaseBehavior,
        IDurationTriggerBehavior
    {
        public DurationTriggerBehavior(IInterval duration)
        {
            Duration = duration;
        }

        public IInterval Duration { get; }
    }
}