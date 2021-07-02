using NexusLabs.Contracts;

namespace ProjectXyz.Plugins.Features.PartyManagement.Default
{
    public sealed class RosterBehaviorFactory : IRosterBehaviorFactory
    {
        public IRosterBehavior Create(
            bool isPartyLeader,
            bool isActiveParty)
        {
            Contract.Requires(
                () => !isPartyLeader || isActiveParty,
                () => $"The behavior must be in the active party if it is expected " +
                $"to be the party leader. Since '{nameof(isPartyLeader)}' was " +
                $"true, did you mean to set '{nameof(isActiveParty)}' to true " +
                $"as well?");

            var rosterBehavior = new RosterBehavior()
            {
                IsPartyLeader = isPartyLeader,
                IsActiveParty = isActiveParty,
            };
            return rosterBehavior;
        }
    }
}
