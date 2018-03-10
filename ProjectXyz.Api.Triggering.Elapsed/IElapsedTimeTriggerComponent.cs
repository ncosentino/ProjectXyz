using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IElapsedTimeTriggerComponent : IComponent
    {
        IInterval Elapsed { get; }
    }
}