using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.ElapsedTime.Api;

namespace ProjectXyz.Plugins.Features.ElapsedTime
{
    public interface IElapsedTimeTriggerSourceMechanicRegistrar :
        IElapsedTimeTriggerSourceMechanic,
        ITriggerMechanicRegistrar
    {
    }
}