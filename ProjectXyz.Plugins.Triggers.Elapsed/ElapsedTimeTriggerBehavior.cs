using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Triggers.Elapsed
{
    public sealed class ElapsedTimeTriggerBehavior :
        BaseBehavior,
        IElapsedTimeTriggerBehavior
    {
        public ElapsedTimeTriggerBehavior(IInterval elapsed)
        {
            Elapsed = elapsed;
        }

        public IInterval Elapsed { get; }
    }
}