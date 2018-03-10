using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Triggers.Elapsed
{
    public sealed class ElapsedTimeTriggerComponent : IElapsedTimeTriggerComponent
    {
        public ElapsedTimeTriggerComponent(IInterval elapsed)
        {
            Elapsed = elapsed;
        }

        public IInterval Elapsed { get; }
    }
}