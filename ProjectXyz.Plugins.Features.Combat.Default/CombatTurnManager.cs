using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;

namespace ProjectXyz.Plugins.Features.Combat.Default
{
    public sealed class CombatTurnManager : ICombatTurnManager
    {
        private readonly ICombatCalculations _combatCalculations;
        private readonly ICombatGameObjectProvider _combatGameObjectProvider;
        private readonly Dictionary<IGameObject, double> _actorCounters;

        public CombatTurnManager(
            ICombatCalculations combatCalculations,
            ICombatGameObjectProvider combatGameObjectProvider)
        {
            _combatCalculations = combatCalculations;
            _combatGameObjectProvider = combatGameObjectProvider;
            _actorCounters = new Dictionary<IGameObject, double>();
        }

        public event EventHandler<TurnOrderEventArgs> TurnProgressed;

        public void Reset()
        {
            _actorCounters.Clear();
        }

        public void ProgressTurn(
            IFilterContext filterContext,
            int turns)
        {
            var actorOrder = CalculateTurnOrder(
                filterContext,
                _actorCounters,
                turns);
            var eventArgs = new TurnOrderEventArgs(actorOrder);
            TurnProgressed?.Invoke(this, eventArgs);
        }

        public IEnumerable<IGameObject> GetSnapshot(
            IFilterContext filterContext,
            int length)
        {
            var actorCounter = _actorCounters.ToDictionary(
                x => x.Key,
                x => x.Value);

            foreach (var actor in CalculateTurnOrder(
                filterContext,
                actorCounter,
                length))
            {
                yield return actor;
            }
        }

        private IEnumerable<IGameObject> CalculateTurnOrder(
            IFilterContext filterContext,
            IDictionary<IGameObject, double> actorCounter,
            int total)
        {
            // FIXME: find ways to incorporate the following
            // - mechanics for missing turns (i.e. stunned, frozen, etc...)
            // - ^^ if any of these are random, we need a way to toggle this
            //   on and off so that the simulation (i.e. getting the
            //   snapshot) doesn't roll them! it would instead show the normal
            //   case scenario.
            // - mechanics means not only plugins for state, but hooks so that
            //   we can trigger things on turn miss etc...

            var actorsCounterPerTurn = new Dictionary<IGameObject, double>();
            var actors = _combatGameObjectProvider
                .GetGameObjects()
                .ToArray();
            var counter = 0;
            while (counter < total)
            {
                foreach (var entry in actors
                    .Select(x => new
                    {
                        Actor = x,
                        Speed = _combatCalculations.CalculateActorIncrementValue(
                            filterContext,
                            actors,
                            x),
                    })
                    .OrderByDescending(x => x.Speed))
                {
                    var actor = entry.Actor;
                    var speed = entry.Speed;

                    if (!actorCounter.ContainsKey(actor))
                    {
                        actorCounter[actor] = 0;
                    }

                    actorCounter[actor] += speed;

                    if (!actorsCounterPerTurn.ContainsKey(actor))
                    {
                        actorsCounterPerTurn[actor] = _combatCalculations.CalculateActorRequiredTargetValuePerTurn(
                            filterContext,
                            actors,
                            actor);
                    }

                    if (actorCounter[actor] >= actorsCounterPerTurn[actor])
                    {
                        actorCounter[actor] -= actorsCounterPerTurn[actor];
                        yield return actor;

                        counter++;
                        if (counter >= total)
                        {
                            yield break;
                        }
                    }
                }
            }
        }
    }
}
