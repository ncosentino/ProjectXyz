using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IElapsedTimeTriggerMechanic : ITriggerMechanic
    {
        bool Update(IInterval elapsed);
    }
}