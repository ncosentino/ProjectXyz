using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed
{
    public interface IElapsedTimeTriggerSourceMechanic : ITriggerSourceMechanic
    {
        void Update(IInterval elapsed);
    }
}