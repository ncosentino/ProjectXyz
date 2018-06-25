using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Api
{
    public interface IElapsedTimeTriggerBehavior : IBehavior
    {
        IInterval Elapsed { get; }
    }
}