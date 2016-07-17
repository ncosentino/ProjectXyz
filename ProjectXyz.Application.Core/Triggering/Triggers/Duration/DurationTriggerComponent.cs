using ProjectXyz.Application.Interface.Triggering.Triggers.Duration;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Triggering.Triggers.Duration
{
    public sealed class DurationTriggerComponent : IDurationTriggerComponent
    {
        public DurationTriggerComponent(IInterval duration)
        {
            Duration = duration;
        }

        public IInterval Duration { get; }
    }
}