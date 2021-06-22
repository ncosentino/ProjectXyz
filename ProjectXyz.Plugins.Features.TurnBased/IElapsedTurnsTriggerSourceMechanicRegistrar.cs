using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedTurnsTriggerSourceMechanicRegistrar :
        IElapsedTurnsTriggerSourceMechanic,
        IDiscoverableTriggerMechanicRegistrar
    {
    }
}