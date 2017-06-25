using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IDurationTriggerComponent : IComponent
    {
        IInterval Duration { get; }
    }
}