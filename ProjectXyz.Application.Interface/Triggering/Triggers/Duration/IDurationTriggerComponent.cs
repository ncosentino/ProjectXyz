using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Triggering.Triggers.Duration
{
    public interface IDurationTriggerComponent : IComponent
    {
        IInterval Duration { get; }
    }
}