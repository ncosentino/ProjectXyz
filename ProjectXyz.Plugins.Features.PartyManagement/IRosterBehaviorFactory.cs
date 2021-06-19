namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public interface IRosterBehaviorFactory
    {
        IRosterBehavior Create(
            bool isPartyLeader,
            bool isActiveParty);
    }
}
