namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public interface IRosterBehavior : IObservableRosterBehavior
    {
        new bool IsActiveParty { get; set; }

        new bool IsPartyLeader { get; set; }
    }
}
