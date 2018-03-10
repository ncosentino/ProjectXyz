using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IDurationTriggerComponent : IComponent
    {
        IInterval Duration { get; }
    }
}