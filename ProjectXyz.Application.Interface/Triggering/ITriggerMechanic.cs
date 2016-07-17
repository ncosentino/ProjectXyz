using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Interface.Triggering
{
    public interface ITriggerMechanic : IMechanic
    {
        bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar);
    }
}