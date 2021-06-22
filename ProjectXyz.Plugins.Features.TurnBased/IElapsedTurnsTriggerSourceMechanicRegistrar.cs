using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedTurnsTriggerSourceMechanicRegistrar :
        IElapsedTurnsTriggerSourceMechanic,
        ITriggerMechanicRegistrar
    {
    }
}