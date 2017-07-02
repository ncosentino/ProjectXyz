using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Triggers.Elapsed
{
    public interface IElapsedTimeTriggerSourceMechanicRegistrar :
        IElapsedTimeTriggerSourceMechanic,
        ITriggerMechanicRegistrar
    {
    }
}