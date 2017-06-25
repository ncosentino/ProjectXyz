using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Triggers.Elapsed.Duration
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