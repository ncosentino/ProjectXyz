using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased.Api
{
    public interface IElapsedTurnsTriggerSourceMechanic : ITriggerSourceMechanic
    {
        void Update(ITurnInfo turnInfo);
    }
}