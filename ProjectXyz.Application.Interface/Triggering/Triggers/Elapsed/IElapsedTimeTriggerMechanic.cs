using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed
{
    public interface IElapsedTimeTriggerMechanic : ITriggerMechanic
    {
        bool Update(IInterval elapsed);
    }
}