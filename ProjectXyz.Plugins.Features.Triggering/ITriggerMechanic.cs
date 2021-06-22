namespace ProjectXyz.Plugins.Features.Triggering
{
    public interface ITriggerMechanic
    {
        bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar);
    }
}