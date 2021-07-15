using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class TurnBasedComponentCreator : IDiscoverableSystemUpdateComponentCreator
    {
        private readonly ITurnInfoFactory _turnInfoFactory;
        private readonly IActionInfoFactory _actionInfoFactory;
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;

        public TurnBasedComponentCreator(
            ITurnBasedManager turnBasedManager,
            IReadOnlyMapGameObjectManager mapGameObjectManager,
            ITurnInfoFactory turnInfoFactory,
            IActionInfoFactory actionInfoFactory)
        {
            _turnBasedManager = turnBasedManager;
            _mapGameObjectManager = mapGameObjectManager;
            _turnInfoFactory = turnInfoFactory;
            _actionInfoFactory = actionInfoFactory;
        }

        public int? Priority => int.MinValue + 1;

        public IEnumerable<IComponent> CreateNext(IReadOnlyCollection<IComponent> components)
        {
            ITurnInfo turnInfo = null;
            IActionInfo actionInfo = null;
            if (_turnBasedManager.SyncTurnsFromElapsedTime)
            {
                turnInfo = GetElapsedTimeTurnInfo(components);
                actionInfo = GetElapsedTimeActionInfo(components);
            }
            // this logical flow should prevent scheduled turns being handled
            // before individual actions
            else if (_turnBasedManager.HasActionTakenQueued)
            {
                actionInfo = GetActionInfo();
            }
            else if (_turnBasedManager.HasTurnTakenQueued)
            {
                turnInfo = GetTurnInfo();
            }
            
            if (turnInfo == null)
            {
                turnInfo = _turnInfoFactory.Create(
                    null,
                    _mapGameObjectManager.GameObjects,
                    0);
            }

            if (actionInfo == null)
            {
                actionInfo = _actionInfoFactory.Create(
                    null,
                    _mapGameObjectManager.GameObjects,
                    0);
            }

            yield return new GenericComponent<ITurnInfo>(turnInfo);
            yield return new GenericComponent<IActionInfo>(actionInfo);
        }

        private ITurnInfo GetElapsedTimeTurnInfo(IReadOnlyCollection<IComponent> components)
        {
            var elapsedTime = components
                .TakeTypes<IComponent<IElapsedTime>>()
                .Single()
                .Value;

            // FIXME: convert time -> turns
            var elapsedTurns = ((IInterval<double>)elapsedTime.Interval).Value / 1000d;

            var turnInfo = _turnInfoFactory.Create(
                null,
                _mapGameObjectManager.GameObjects,
                elapsedTurns);
            return turnInfo;
        }

        private IActionInfo GetElapsedTimeActionInfo(IReadOnlyCollection<IComponent> components)
        {
            var elapsedTime = components
                .TakeTypes<IComponent<IElapsedTime>>()
                .Single()
                .Value;

            // FIXME: convert time -> actions
            var elapsedActions = ((IInterval<double>)elapsedTime.Interval).Value / 1000d;

            var actionInfo = _actionInfoFactory.Create(
                null,
                _mapGameObjectManager.GameObjects,
                elapsedActions);
            return actionInfo;
        }

        private IActionInfo GetActionInfo()
        {
            var actionTakenInfo = _turnBasedManager.GetNextActionTaken();
            var actionInfo = _actionInfoFactory.Create(
                actionTakenInfo.Actor,
                actionTakenInfo.ApplicableGameObjects,
                1);
            return actionInfo;
        }

        private ITurnInfo GetTurnInfo()
        {
            var turnTakenInfo = _turnBasedManager.GetNextTurnTaken();
            var turnInfo = _turnInfoFactory.Create(
                turnTakenInfo.Actor,
                turnTakenInfo.ApplicableGameObjects,
                1);
            return turnInfo;
        }
    }
}
