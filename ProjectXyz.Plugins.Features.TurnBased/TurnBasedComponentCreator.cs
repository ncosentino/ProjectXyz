using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public sealed class TurnBasedComponentCreator : IDiscoverableSystemUpdateComponentCreator
    {
        private readonly ITurnBasedManager _turnBasedManager;

        public TurnBasedComponentCreator(ITurnBasedManager turnBasedManager)
        {
            _turnBasedManager = turnBasedManager;
        }

        public int? Priority => int.MinValue + 1;

        public IEnumerable<IComponent> CreateNext(IReadOnlyCollection<IComponent> components)
        {
            var applicableGameObjects = _turnBasedManager.GetApplicableGameObjects();

            double elapsedTurns;
            if (_turnBasedManager.SyncTurnsFromElapsedTime)
            {
                var elapsedTime = components
                    .TakeTypes<IComponent<IElapsedTime>>()
                    .Single()
                    .Value;

                // FIXME: convert time -> turns
                elapsedTurns = ((IInterval<double>)elapsedTime.Interval).Value / 1000d;
            }
            else
            {
                elapsedTurns = applicableGameObjects.Count > 0
                    ? 1
                    : 0;
            }

            var component = new GenericComponent<ITurnInfo>(new TurnInfo(
                applicableGameObjects,
                elapsedTurns,
                _turnBasedManager.GlobalSync));
            if (_turnBasedManager.ClearApplicableOnUpdate)
            {
                _turnBasedManager.SetApplicableObjects(Enumerable.Empty<IGameObject>());
            }

            yield return component;
        }
    }
}
