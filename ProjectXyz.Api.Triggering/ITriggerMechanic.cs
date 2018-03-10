using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Triggering
{
    public interface ITriggerMechanic : IMechanic
    {
        bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar);
    }
}