using System.Collections.Concurrent;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class TurnBasedManager : ITurnBasedManager
    {
        private readonly ConcurrentQueue<IActionTakenInfo> _queuedActionTaken;
        private readonly ConcurrentQueue<ITurnTakenInfo> _queuedTurnTaken;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;

        public TurnBasedManager(IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            SyncTurnsFromElapsedTime = true;

            _queuedActionTaken = new ConcurrentQueue<IActionTakenInfo>();
            _queuedTurnTaken = new ConcurrentQueue<ITurnTakenInfo>();
            _mapGameObjectManager = mapGameObjectManager;
        }

        public bool SyncTurnsFromElapsedTime { get; set; }

        public bool HasTurnTakenQueued => !_queuedTurnTaken.IsEmpty;

        public bool HasActionTakenQueued => !_queuedActionTaken.IsEmpty;

        public ITurnTakenInfo GetNextTurnTaken()
        {
            Contract.Requires(
                _queuedTurnTaken.TryDequeue(out var info),
                $"Could not dequeue turn taken info.");
            return info;
        }

        public IActionTakenInfo GetNextActionTaken()
        {
            Contract.Requires(
                _queuedActionTaken.TryDequeue(out var info),
                $"Could not dequeue action taken info.");
            return info;
        }

        public void NotifyActionTaken(IGameObject actor)
        {
            var info = new ActionTakenInfo(
                actor,
                _mapGameObjectManager.GameObjects);
            _queuedActionTaken.Enqueue(info);
        }

        public void NotifyTurnTaken(IGameObject actor)
        {
            var info = new TurnTakenInfo(
                actor, 
                _mapGameObjectManager.GameObjects);
            _queuedTurnTaken.Enqueue(info);
        }
    }
}
