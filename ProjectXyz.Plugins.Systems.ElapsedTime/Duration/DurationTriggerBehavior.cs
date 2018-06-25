using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.ElapsedTime.Api.Duration;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Duration
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