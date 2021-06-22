namespace ProjectXyz.Plugins.Features.Triggering
{
    public interface ITriggerMechanicRegistrar
    {
        bool CanRegister(ITriggerMechanic triggerMechanic);

        void RegisterTrigger(ITriggerMechanic triggerMechanic);

        void UnregisterTrigger(ITriggerMechanic triggerMechanic);
    }
}