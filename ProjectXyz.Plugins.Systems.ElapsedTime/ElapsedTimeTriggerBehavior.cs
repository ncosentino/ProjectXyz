using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.ElapsedTime.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.ElapsedTime
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