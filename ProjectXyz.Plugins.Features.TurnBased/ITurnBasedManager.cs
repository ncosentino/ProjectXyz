using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface ITurnBasedManager : IReadOnlyTurnBasedManager
    {
        new bool SyncTurnsFromElapsedTime { get; set; }

        void NotifyActionTaken(IGameObject actor);

        void NotifyTurnTaken(IGameObject actor);
    }
}
