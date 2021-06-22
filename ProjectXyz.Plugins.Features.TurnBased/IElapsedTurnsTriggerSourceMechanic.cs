using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedTurnsTriggerSourceMechanic : ITriggerSourceMechanic
    {
        void Update(ITurnInfo turnInfo);
    }
}