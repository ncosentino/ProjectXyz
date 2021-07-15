using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedActionsTriggerSourceMechanicRegistrar :
        IElapsedActionsTriggerSourceMechanic,
        IDiscoverableTriggerMechanicRegistrar
    {
    }
}