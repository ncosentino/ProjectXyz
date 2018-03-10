using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Triggering.Elapsed
{
    public interface IElapsedTimeTriggerSourceMechanic : ITriggerSourceMechanic
    {
        void Update(IInterval elapsed);
    }
}