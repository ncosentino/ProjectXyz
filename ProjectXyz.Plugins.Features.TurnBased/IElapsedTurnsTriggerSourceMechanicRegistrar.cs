using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedTurnsTriggerSourceMechanicRegistrar :
        IElapsedTurnsTriggerSourceMechanic,
        ITriggerMechanicRegistrar
    {
    }
}