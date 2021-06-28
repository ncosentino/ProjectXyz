using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.PartyManagement.Default
{
    public sealed class RosterManager : IRosterManager
    {
        private readonly Dictionary<IGameObject, IRosterBehavior> _roster;

        private IGameObject _partyLeader;

        public RosterManager()
        {
            _roster = new Dictionary<IGameObject, IRosterBehavior>();
        }

        public event EventHandler PartyLeaderChanged;

        public event EventHandler ActivePartyChanged;

        public event EventHandler ControlledActorChanged;

        public event EventHandler RosterChanged;

        public IGameObject ActivePartyLeader
        {
            get => _partyLeader;
            set
            {
                if (_partyLeader == value)
                {
                    return;
                }

                _partyLeader = value;
                PartyLeaderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public IEnumerable<IGameObject> ActiveParty => FullRoster.Where(x => x.GetOnly<IRosterBehavior>().IsActiveParty);

        public IGameObject CurrentlyControlledActor => ActiveParty.SingleOrDefault(x => 
            x.TryGetFirst<IPlayerControlledBehavior>(out var playerControlledBehavior) &&
            playerControlledBehavior.IsActive);

        public IReadOnlyCollection<IGameObject> FullRoster => _roster.Keys;

        public bool ExistsInRoster(IGameObject actor) => _roster.ContainsKey(actor);

        public bool ExistsInActiveParty(IGameObject actor) =>
            ExistsInRoster(actor) &&
            _roster[actor].IsActiveParty;

        public void AddToRoster(IGameObject actor)
        {
            if (_roster.ContainsKey(actor))
            {
                return;
            }

            var rosterBehavior = actor.GetOnly<IRosterBehavior>();

            var shouldBeNewPartyLeader = rosterBehavior.IsPartyLeader;
            var shouldBeInActiveParty = shouldBeNewPartyLeader || rosterBehavior.IsActiveParty;

            // reset this before hooking up events
            if (shouldBeInActiveParty)
            {
                rosterBehavior.IsActiveParty = false;
                rosterBehavior.IsPartyLeader = false;
            }

            rosterBehavior.IsActivePartyChanged += RosterBehavior_IsActivePartyChanged;
            rosterBehavior.IsPartyLeaderChanged += RosterBehavior_IsPartyLeaderChanged;

            _roster.Add(actor, rosterBehavior);
            RosterChanged?.Invoke(this, EventArgs.Empty);

            if (shouldBeNewPartyLeader)
            {
                rosterBehavior.IsPartyLeader = true;
            }
            else if (shouldBeInActiveParty)
            {
                rosterBehavior.IsActiveParty = true;
            }
        }

        public void RemoveFromRoster(IGameObject actor)
        {
            if (!_roster.ContainsKey(actor))
            {
                return;
            }

            var rosterBehavior = actor.GetOnly<IRosterBehavior>();

            // force remove them from the party if they were in the party
            rosterBehavior.IsActiveParty = false;

            rosterBehavior.IsActivePartyChanged -= RosterBehavior_IsActivePartyChanged;
            rosterBehavior.IsPartyLeaderChanged -= RosterBehavior_IsPartyLeaderChanged;

            _roster.Remove(actor);
            RosterChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ClearRoster()
        {
            foreach (var actor in _roster.Keys.ToArray())
            {
                RemoveFromRoster(actor);
            }
        }

        public void SetActorToControl(IGameObject targetActor)
        {
            var lastControlled = CurrentlyControlledActor;

            foreach (var actor in FullRoster)
            {
                if (!actor.TryGetFirst<IPlayerControlledBehavior>(out var playerControlledBehavior))
                {
                    continue;
                }

                if (playerControlledBehavior == null)
                {
                    continue;
                }

                var active = actor == targetActor;
                playerControlledBehavior.IsActive = active;
            }

            if (lastControlled != CurrentlyControlledActor)
            {
                ControlledActorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void RosterBehavior_IsPartyLeaderChanged(object sender, EventArgs e)
        {
            var rosterBehavior = (IRosterBehavior)sender;
            if (!rosterBehavior.IsPartyLeader)
            {
                var activeParty = ActiveParty.Where(x => x != rosterBehavior.Owner).ToArray();
                if (activeParty.Any() && !activeParty.Any(x => _roster[x].IsPartyLeader))
                {
                    _roster[activeParty.First()].IsPartyLeader = true;
                }

                if (!activeParty.Any())
                {
                    ActivePartyLeader = null;
                }

                return;
            }

            foreach (var otherActor in ActiveParty.Where(x => x != rosterBehavior.Owner))
            {
                _roster[otherActor].IsPartyLeader = false;
            }

            rosterBehavior.IsActiveParty = true;
            ActivePartyLeader = rosterBehavior.Owner;
        }

        private void RosterBehavior_IsActivePartyChanged(object sender, EventArgs e)
        {
            var rosterBehavior = (IRosterBehavior)sender;
            if (rosterBehavior.IsActiveParty && ActivePartyLeader == null)
            {
                rosterBehavior.IsPartyLeader = true;
            }
                
            ActivePartyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
