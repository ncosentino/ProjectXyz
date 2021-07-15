namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IReadOnlyTurnBasedManager
    {
        bool SyncTurnsFromElapsedTime { get; }

        bool HasTurnTakenQueued { get; }

        bool HasActionTakenQueued { get; }

        ITurnTakenInfo GetNextTurnTaken();

        IActionTakenInfo GetNextActionTaken();
    }
}
