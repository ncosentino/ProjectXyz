using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Api
{
    public interface IElapsedTimeTriggerMechanic : ITriggerMechanic
    {
        bool Update(IInterval elapsed);
    }
}