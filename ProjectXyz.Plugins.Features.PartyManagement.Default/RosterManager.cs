using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.PartyManagement.Default
{
    public sealed class RosterManager : IRosterManager
    {
        private readonly ConcurrentDictionary<IGameObject, RosterEntry> _roster;

        private IGameObject _partyLeader;
        private IGameObject _controlledActor;

        public RosterManager()
        {
            _roster = new ConcurrentDictionary<IGameObject, RosterEntry>();
        }

        public event EventHandler PartyLeaderChanged;

        public event EventHandler ActivePartyChanged;

        public event EventHandler ControlledActorChanged;

        public event EventHandler RosterChanged;

        public IGameObject ActivePartyLeader
        {
            get => _partyLeader;
            private set
            {
                if (_partyLeader == value)
                {
                    return;
                }

                _partyLeader = value;
                PartyLeaderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public IGameObject ActiveControlledActor
        {
            get => _controlledActor;
            private set
            {
                if (_controlledActor == value)
                {
                    return;
                }

                _controlledActor = value;
                ControlledActorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public IReadOnlyCollection<IGameObject> ActiveParty => FullRoster
            .Where(x => x.GetOnly<IRosterBehavior>().IsActiveParty)
            .ToArray();

        public IReadOnlyCollection<IGameObject> FullRoster => _roster
            .Keys
            .Cast<IGameObject>()
            .ToArray();

        public bool ExistsInRoster(IGameObject actor) => _roster.ContainsKey(actor);

        public bool ExistsInActiveParty(IGameObject actor) =>
            ExistsInRoster(actor) &&
            _roster[actor].RosterBehavior.IsActiveParty;

        public void AddToRoster(IGameObject actor)
        {
            if (_roster.ContainsKey(actor))
            {
                return;
            }

            var rosterBehavior = actor.GetOnly<IRosterBehavior>();
            var playerControlledBehavior = actor.GetOnly<IPlayerControlledBehavior>();

            var shouldBeNewPartyLeader = rosterBehavior.IsPartyLeader;
            var shouldBeNewControlled = playerControlledBehavior.IsActive;
            var shouldBeInActiveParty = 
                shouldBeNewPartyLeader || 
                shouldBeNewControlled ||
                rosterBehavior.IsActiveParty;

            // reset this before hooking up events
            if (shouldBeInActiveParty)
            {
                rosterBehavior.IsActiveParty = false;
                rosterBehavior.IsPartyLeader = false;
            }

            rosterBehavior.IsActivePartyChanged += RosterBehavior_IsActivePartyChanged;
            rosterBehavior.IsPartyLeaderChanged += RosterBehavior_IsPartyLeaderChanged;
            playerControlledBehavior.IsActiveChanged += PlayerControlledBehavior_IsActiveChanged;

            Contract.Requires(
                _roster.TryAdd(actor, new RosterEntry(rosterBehavior, playerControlledBehavior)),
                $"Could not add '{actor}' to the roster.");
            RosterChanged?.Invoke(this, EventArgs.Empty);

            if (shouldBeNewPartyLeader)
            {
                rosterBehavior.IsPartyLeader = true;
            }
            else if (shouldBeInActiveParty)
            {
                rosterBehavior.IsActiveParty = true;
            }

            if (shouldBeNewControlled)
            {
                playerControlledBehavior.IsActive = true;
            }
        }

        public void RemoveFromRoster(IGameObject actor)
        {
            if (!_roster.ContainsKey(actor))
            {
                return;
            }

            var rosterBehavior = actor.GetOnly<IRosterBehavior>();
            var playerControlledBehavior = actor.GetOnly<IPlayerControlledBehavior>();

            // force remove them from the party if they were in the party
            rosterBehavior.IsActiveParty = false;
            playerControlledBehavior.IsActive = false;

            rosterBehavior.IsActivePartyChanged -= RosterBehavior_IsActivePartyChanged;
            rosterBehavior.IsPartyLeaderChanged -= RosterBehavior_IsPartyLeaderChanged;
            playerControlledBehavior.IsActiveChanged -= PlayerControlledBehavior_IsActiveChanged;

            _roster.TryRemove(actor, out _);
            RosterChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ClearRoster()
        {
            foreach (var actor in _roster.Keys.ToArray())
            {
                RemoveFromRoster(actor);
            }
        }

        private void RosterBehavior_IsPartyLeaderChanged(
            object sender,
            EventArgs e)
        {
            var rosterBehavior = (IRosterBehavior)sender;
            if (!rosterBehavior.IsPartyLeader)
            {
                var activeParty = ActiveParty.ToArray();
                var activePartyExceptCurrent = activeParty
                    .Where(x => x != rosterBehavior.Owner)
                    .ToArray();
                if (activePartyExceptCurrent.Any() && !activePartyExceptCurrent.Any(x => _roster[x].RosterBehavior.IsPartyLeader))
                {
                    _roster[activePartyExceptCurrent.First()].RosterBehavior.IsPartyLeader = true;
                }

                if (activeParty.Any() && !activeParty.Any(x => _roster[x].PlayerControlledBehavior.IsActive))
                {
                    _roster[activeParty.First()].PlayerControlledBehavior.IsActive = true;
                }

                if (!activePartyExceptCurrent.Any())
                {
                    ActivePartyLeader = null;
                    ActiveControlledActor = null;
                }

                return;
            }

            foreach (var otherActor in ActiveParty.Where(x => x != rosterBehavior.Owner))
            {
                _roster[otherActor].RosterBehavior.IsPartyLeader = false;
            }

            rosterBehavior.IsActiveParty = true;
            ActivePartyLeader = rosterBehavior.Owner;
        }

        private void RosterBehavior_IsActivePartyChanged(
            object sender,
            EventArgs e)
        {
            var rosterBehavior = (IRosterBehavior)sender;
            if (rosterBehavior.IsActiveParty && ActivePartyLeader == null)
            {
                rosterBehavior.IsPartyLeader = true;
            }

            if (rosterBehavior.IsActiveParty && ActiveControlledActor == null)
            {
                _roster[rosterBehavior.Owner].PlayerControlledBehavior.IsActive = true;
            }

            ActivePartyChanged?.Invoke(this, EventArgs.Empty);
        }

        private void PlayerControlledBehavior_IsActiveChanged(
            object sender,
            EventArgs e)
        {
            var playerControlledBehavior = (IPlayerControlledBehavior)sender;
            if (!playerControlledBehavior.IsActive)
            {
                var activeParty = ActiveParty.ToArray();
                var activePartyExceptCurrent = activeParty
                    .Where(x => x != playerControlledBehavior.Owner)
                    .ToArray();
                if (activePartyExceptCurrent.Any() && !activePartyExceptCurrent.Any(x => _roster[x].RosterBehavior.IsPartyLeader))
                {
                    _roster[activePartyExceptCurrent.First()].RosterBehavior.IsPartyLeader = true;
                }

                if (activeParty.Any() && !activeParty.Any(x => _roster[x].PlayerControlledBehavior.IsActive))
                {
                    _roster[activeParty.First()].PlayerControlledBehavior.IsActive = true;
                }

                if (!activePartyExceptCurrent.Any())
                {
                    ActivePartyLeader = null;
                    ActiveControlledActor = null;
                }

                return;
            }

            foreach (var otherActor in ActiveParty.Where(x => x != playerControlledBehavior.Owner))
            {
                _roster[otherActor].PlayerControlledBehavior.IsActive = false;
            }

            playerControlledBehavior.IsActive = true;
            ActiveControlledActor = playerControlledBehavior.Owner;
        }

        private sealed class RosterEntry
        {
            public RosterEntry(
                IRosterBehavior rosterBehavior,
                IPlayerControlledBehavior playerControlledBehavior)
            {
                RosterBehavior = rosterBehavior;
                PlayerControlledBehavior = playerControlledBehavior;
            }

            public IRosterBehavior RosterBehavior { get; }

            public IPlayerControlledBehavior PlayerControlledBehavior { get; }
        }
    }
}
