using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedTurnsTriggerMechanic : ITriggerMechanic
    {
        bool Update(ITurnInfo turnInfo);
    }
}