﻿using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

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

        public event EventHandler<TurnProgressedEventArgs> TurnProgressed;

        public event EventHandler<CombatStartedEventArgs> CombatStarted;

        public event EventHandler<CombatEndedEventArgs> CombatEnded;

        public bool InCombat { get; private set; }

        public void StartCombat(IFilterContext filterContext)
        {
            _actorCounters.Clear();

            var actorOrder = GetSnapshot(filterContext, 1);
            var eventArgs = new CombatStartedEventArgs(actorOrder);

            InCombat = true;
            CombatStarted?.Invoke(this, eventArgs);
        }

        public void EndCombat(
            IEnumerable<IGameObject> winningTeam,
            IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams)
        {
            _actorCounters.Clear();
            var eventArgs = new CombatEndedEventArgs(
                winningTeam,
                losingTeams);

            InCombat = false;
            CombatEnded?.Invoke(this, eventArgs);
        }

        public void ProgressTurn(
            IFilterContext filterContext,
            int turns)
        {
            Contract.RequiresNotNull(
                filterContext,
                $"{nameof(filterContext)} cannot be null.");
            Contract.Requires(
                turns > 0,
                $"{nameof(turns)} must be greater than 0.");

            var actorOrder = CalculateTurnOrder(
                filterContext,
                _actorCounters,
                turns)
                .ToArray();
            var eventArgs = new TurnProgressedEventArgs(
                actorOrder,
                GetSnapshot(filterContext, 1).Single());
            TurnProgressed?.Invoke(this, eventArgs);
        }

        public IEnumerable<IGameObject> GetSnapshot(
            IFilterContext filterContext,
            int length)
        {
            Contract.RequiresNotNull(
                filterContext,
                $"{nameof(filterContext)} cannot be null.");
            Contract.Requires(
                length > 0,
                $"{nameof(length)} must be greater than 0.");

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
