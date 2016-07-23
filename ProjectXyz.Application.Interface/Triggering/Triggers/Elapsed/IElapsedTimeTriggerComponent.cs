using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed
{
    public interface IElapsedTimeTriggerComponent : IComponent
    {
        IInterval Elapsed { get; }
    }
}