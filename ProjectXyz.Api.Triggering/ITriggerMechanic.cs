using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Api.Triggering
{
    public interface ITriggerMechanic : IMechanic
    {
        bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar);
    }
}