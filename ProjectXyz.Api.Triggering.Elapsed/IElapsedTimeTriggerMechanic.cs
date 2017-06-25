using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IElapsedTimeTriggerMechanic : ITriggerMechanic
    {
        bool Update(IInterval elapsed);
    }
}