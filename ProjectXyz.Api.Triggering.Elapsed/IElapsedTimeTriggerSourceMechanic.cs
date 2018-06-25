using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Api
{
    public interface IElapsedTimeTriggerSourceMechanic : ITriggerSourceMechanic
    {
        void Update(IInterval elapsed);
    }
}