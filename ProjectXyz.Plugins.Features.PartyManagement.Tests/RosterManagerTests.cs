using System;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Behaviors.Default;
using ProjectXyz.Plugins.Features.GameObjects.Actors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.PartyManagement.Default;

using Xunit;

namespace ProjectXyz.Plugins.Features.PartyManagement.Tests
{
    public sealed class RosterManagerTests
    {
        private readonly RosterManager _rosterManager;

        public RosterManagerTests()
        {
            _rosterManager = new RosterManager();
        }

        [Fact]
        private void AddToRoster_NoBehaviors_ThrowsException()
        {
            var actor = new GameObject(new IBehavior[]
            {
            });

            Assert.Throws<ContractException>(() => _rosterManager.AddToRoster(actor));            
        }

        [Fact]
        private void AddToRoster_NoPlayerControlledBehavior_ThrowsException()
        {
            var actor = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
            });

            Assert.Throws<ContractException>(() => _rosterManager.AddToRoster(actor));
        }

        [Fact]
        private void AddToRoster_NoRosterBehavior_ThrowsException()
        {
            var actor = new GameObject(new IBehavior[]
            {
                new PlayerControlledBehavior(),
            });

            Assert.Throws<ContractException>(() => _rosterManager.AddToRoster(actor));
        }

        [Fact]
        private void AddToRoster_SingleActor_SingleRosterEmptyActiveParty()
        {
            var actor = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            _rosterManager.AddToRoster(actor);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(0, partyLeaderChangedCount);
            Assert.Equal(0, controlledActorChangedCount);
            Assert.Equal(0, activePartyChangedCount);
            Assert.False(
                actor.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor}' to be the party leader.");
            Assert.False(
                actor.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor}' to be in the active party.");
            Assert.Null(_rosterManager.ActivePartyLeader);
            Assert.Null(_rosterManager.ActiveControlledActor);
            Assert.Empty(_rosterManager.ActiveParty);
            Assert.Equal(actor, Assert.Single(_rosterManager.FullRoster));
        }

        [Fact]
        private void AddToRoster_TwoActorsNoneInParty_TwoRosterEmptyParty()
        {
            var actor1 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });
            var actor2 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            _rosterManager.AddToRoster(actor1);
            _rosterManager.AddToRoster(actor2);

            Assert.Equal(2, rosterChangedCount);
            Assert.Equal(0, partyLeaderChangedCount);
            Assert.Equal(0, controlledActorChangedCount);
            Assert.Equal(0, activePartyChangedCount);
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor1}' to be the party leader.");
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor1}' to be in the active party.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor2}' to be the party leader.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor2}' to be in the active party.");
            Assert.Null(_rosterManager.ActivePartyLeader);
            Assert.Null(_rosterManager.ActiveControlledActor);
            Assert.Empty(_rosterManager.ActiveParty);
            Assert.Equal(2, _rosterManager.FullRoster.Count);
            Assert.Contains(actor1, _rosterManager.FullRoster);
            Assert.Contains(actor2, _rosterManager.FullRoster);
        }

        [Fact]
        private void AddToRoster_ExistingPartyLeaderNewBehaviorSuggestsLeader_NewActorReplacesOldLeader()
        {
            var actor1 = new GameObject(new IBehavior[]
            {
                new RosterBehavior()
                {
                    IsActiveParty = true,
                    IsPartyLeader = true,
                },
                new PlayerControlledBehavior(),
            });
            var actor2 = new GameObject(new IBehavior[]
            {
                new RosterBehavior()
                {
                    IsActiveParty = true,
                    IsPartyLeader = true,
                },
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor1);

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            _rosterManager.AddToRoster(actor2);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(1, partyLeaderChangedCount);
            Assert.Equal(0, controlledActorChangedCount);
            Assert.Equal(1, activePartyChangedCount);
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor1}' to be the party leader.");
            Assert.True(
                actor1.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Expecting '{actor1}' to be in the active party.");
            Assert.True(
                actor2.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Expecting '{actor2}' to be the party leader.");
            Assert.True(
                actor2.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Expecting '{actor2}' to be in the active party.");
            Assert.Equal(actor2, _rosterManager.ActivePartyLeader);
            Assert.Equal(actor1, _rosterManager.ActiveControlledActor);
            Assert.Equal(2, _rosterManager.ActiveParty.Count());
            Assert.Contains(actor1, _rosterManager.ActiveParty);
            Assert.Contains(actor2, _rosterManager.ActiveParty);
            Assert.Equal(2, _rosterManager.FullRoster.Count);
            Assert.Contains(actor1, _rosterManager.FullRoster);
            Assert.Contains(actor2, _rosterManager.FullRoster);
        }

        [Fact]
        private void RemoveFromRoster_NoActivePartyLastActor_EmptyPartyEmptyRoster()
        {
            var actor = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor);

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            _rosterManager.RemoveFromRoster(actor);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(0, partyLeaderChangedCount);
            Assert.Equal(0, controlledActorChangedCount);
            Assert.Equal(0, activePartyChangedCount);
            Assert.False(
                actor.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor}' to be the party leader.");
            Assert.False(
                actor.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor}' to be in the active party.");
            Assert.Null(_rosterManager.ActivePartyLeader);
            Assert.Null(_rosterManager.ActiveControlledActor);
            Assert.Empty(_rosterManager.ActiveParty);
            Assert.Empty(_rosterManager.FullRoster);
        }

        [Fact]
        private void RemoveFromRoster_ActivePartyLastActor_EmptyPartyEmptyRoster()
        {
            var actor = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor);
            actor.GetOnly<IRosterBehavior>().IsActiveParty = true;

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            _rosterManager.RemoveFromRoster(actor);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(1, partyLeaderChangedCount);
            Assert.Equal(1, controlledActorChangedCount);
            Assert.Equal(1, activePartyChangedCount);
            Assert.False(
                actor.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor}' to be the party leader.");
            Assert.False(
                actor.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor}' to be in the active party.");
            Assert.Null(_rosterManager.ActivePartyLeader);
            Assert.Null(_rosterManager.ActiveControlledActor);
            Assert.Empty(_rosterManager.ActiveParty);
            Assert.Empty(_rosterManager.FullRoster);
        }

        [Fact]
        private void RemoveFromRoster_NotLastActorNotLeader_EmptyPartySingleRoster()
        {
            var actor1 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });
            var actor2 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor1);
            _rosterManager.AddToRoster(actor2);

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            _rosterManager.RemoveFromRoster(actor2);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(0, partyLeaderChangedCount);
            Assert.Equal(0, controlledActorChangedCount);
            Assert.Equal(0, activePartyChangedCount);
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor1}' to be the party leader.");
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor1}' to be in the active party.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor2}' to be the party leader.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor2}' to be in the active party.");
            Assert.Null(_rosterManager.ActivePartyLeader);
            Assert.Null(_rosterManager.ActiveControlledActor);
            Assert.Empty(_rosterManager.ActiveParty);
            Assert.Equal(actor1, Assert.Single(_rosterManager.FullRoster));
        }

        [Fact]
        private void RemoveFromRoster_MultipleRosterSinglePartyWasLeader_NoNewLeaderNoActiveParty()
        {
            var actor1 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });
            var actor2 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor1);
            _rosterManager.AddToRoster(actor2);

            actor2.GetOnly<IRosterBehavior>().IsPartyLeader = true;

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            _rosterManager.RemoveFromRoster(actor2);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(1, partyLeaderChangedCount);
            Assert.Equal(1, controlledActorChangedCount);
            Assert.Equal(1, activePartyChangedCount);
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor1}' to be the party leader.");
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor1}' to be in the active party.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor2}' to be the party leader.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor2}' to be in the active party.");
            Assert.Null(_rosterManager.ActivePartyLeader);
            Assert.Null(_rosterManager.ActiveControlledActor);
            Assert.Empty(_rosterManager.ActiveParty);
            Assert.Equal(actor1, Assert.Single(_rosterManager.FullRoster));
        }

        [Fact]
        private void RemoveFromRoster_MultiplePartyWasLeaderNotControlled_NewLeaderSingleActiveParty()
        {
            var actor1 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });
            var actor2 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor1);
            _rosterManager.AddToRoster(actor2);

            actor1.GetOnly<IRosterBehavior>().IsActiveParty = true;
            actor2.GetOnly<IRosterBehavior>().IsPartyLeader = true;
            Assert.Equal(actor2, _rosterManager.ActivePartyLeader);
            Assert.Equal(actor1, _rosterManager.ActiveControlledActor);

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            // ensure our expected state before performing our test's action
            Assert.Equal(actor2, _rosterManager.ActivePartyLeader);
            Assert.Equal(2, _rosterManager.ActiveParty.Count());
            Assert.Contains(actor1, _rosterManager.ActiveParty);
            Assert.Contains(actor2, _rosterManager.ActiveParty);

            _rosterManager.RemoveFromRoster(actor2);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(1, partyLeaderChangedCount);
            Assert.Equal(0, controlledActorChangedCount);
            Assert.Equal(1, activePartyChangedCount);
            Assert.True(
                actor1.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Expecting '{actor1}' to be the party leader.");
            Assert.True(
                actor1.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Expecting '{actor1}' to be in the active party.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor2}' to be the party leader.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor2}' to be in the active party.");
            Assert.Equal(actor1, _rosterManager.ActivePartyLeader);
            Assert.Equal(actor1, _rosterManager.ActiveControlledActor);
            Assert.Equal(actor1, Assert.Single(_rosterManager.ActiveParty));
            Assert.Equal(actor1, Assert.Single(_rosterManager.FullRoster));
        }

        [Fact]
        private void RemoveFromRoster_MultiplePartyWasLeaderAndControlled_NewLeaderAndControlledSingleActiveParty()
        {
            var actor1 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });
            var actor2 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor1);
            _rosterManager.AddToRoster(actor2);

            actor1.GetOnly<IRosterBehavior>().IsActiveParty = true;
            actor2.GetOnly<IRosterBehavior>().IsPartyLeader = true;
            actor2.GetOnly<IPlayerControlledBehavior>().IsActive = true;
            Assert.Equal(actor2, _rosterManager.ActivePartyLeader);
            Assert.Equal(actor2, _rosterManager.ActiveControlledActor);

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            // ensure our expected state before performing our test's action
            Assert.Equal(actor2, _rosterManager.ActivePartyLeader);
            Assert.Equal(2, _rosterManager.ActiveParty.Count());
            Assert.Contains(actor1, _rosterManager.ActiveParty);
            Assert.Contains(actor2, _rosterManager.ActiveParty);

            _rosterManager.RemoveFromRoster(actor2);

            Assert.Equal(1, rosterChangedCount);
            Assert.Equal(1, partyLeaderChangedCount);
            Assert.Equal(1, controlledActorChangedCount);
            Assert.Equal(1, activePartyChangedCount);
            Assert.True(
                actor1.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Expecting '{actor1}' to be the party leader.");
            Assert.True(
                actor1.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Expecting '{actor1}' to be in the active party.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor2}' to be the party leader.");
            Assert.False(
                actor2.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor2}' to be in the active party.");
            Assert.Equal(actor1, _rosterManager.ActivePartyLeader);
            Assert.Equal(actor1, _rosterManager.ActiveControlledActor);
            Assert.Equal(actor1, Assert.Single(_rosterManager.ActiveParty));
            Assert.Equal(actor1, Assert.Single(_rosterManager.FullRoster));
        }

        [Fact]
        private void SetIsPartyLeaderOnBehavior_MultipleRosterNoActiveParty_NewLeaderSingleActiveParty()
        {
            var actor1 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });
            var actor2 = new GameObject(new IBehavior[]
            {
                new RosterBehavior(),
                new PlayerControlledBehavior(),
            });

            _rosterManager.AddToRoster(actor1);
            _rosterManager.AddToRoster(actor2);

            var partyLeaderChangedCount = 0;
            _rosterManager.PartyLeaderChanged += (s, e) => partyLeaderChangedCount++;

            var controlledActorChangedCount = 0;
            _rosterManager.ControlledActorChanged += (s, e) => controlledActorChangedCount++;

            var rosterChangedCount = 0;
            _rosterManager.RosterChanged += (s, e) => rosterChangedCount++;

            var activePartyChangedCount = 0;
            _rosterManager.ActivePartyChanged += (s, e) => activePartyChangedCount++;

            actor2.GetOnly<IRosterBehavior>().IsPartyLeader = true;

            Assert.Equal(0, rosterChangedCount);
            Assert.Equal(1, partyLeaderChangedCount);
            Assert.Equal(1, controlledActorChangedCount);
            Assert.Equal(1, activePartyChangedCount);
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Not expecting '{actor1}' to be the party leader.");
            Assert.False(
                actor1.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Not expecting '{actor1}' to be in the active party.");
            Assert.True(
                actor2.GetOnly<IRosterBehavior>().IsPartyLeader,
                $"Expecting '{actor2}' to be the party leader.");
            Assert.True(
                actor2.GetOnly<IRosterBehavior>().IsActiveParty,
                $"Expecting '{actor2}' to be in the active party.");
            Assert.Equal(actor2, _rosterManager.ActivePartyLeader);
            Assert.Equal(actor2, _rosterManager.ActiveControlledActor);
            Assert.Equal(actor2, Assert.Single(_rosterManager.ActiveParty));
            Assert.Equal(2, _rosterManager.FullRoster.Count);
            Assert.Contains(actor1, _rosterManager.FullRoster);
            Assert.Contains(actor2, _rosterManager.FullRoster);
        }
    }
}
