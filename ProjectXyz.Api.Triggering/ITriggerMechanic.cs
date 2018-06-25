namespace ProjectXyz.Api.Triggering
{
    public interface ITriggerMechanic
    {
        bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar);
    }
}