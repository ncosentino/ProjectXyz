using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IElapsedTimeTriggerComponent : IComponent
    {
        IInterval Elapsed { get; }
    }
}