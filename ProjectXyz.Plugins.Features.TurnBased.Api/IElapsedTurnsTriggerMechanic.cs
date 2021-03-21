using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased.Api
{
    public interface IElapsedTurnsTriggerMechanic : ITriggerMechanic
    {
        bool Update(ITurnInfo turnInfo);
    }
}