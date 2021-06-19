namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public static class IRosterBehaviorFactoryExtensions
    {
        public static IRosterBehavior CreateActivePartyLeader(this IRosterBehaviorFactory rosterBehaviorFactory) =>
            rosterBehaviorFactory.Create(true, true);

        public static IRosterBehavior CreateActivePartyMember(this IRosterBehaviorFactory rosterBehaviorFactory) =>
            rosterBehaviorFactory.Create(false, true);

        public static IRosterBehavior CreateRosterMember(this IRosterBehaviorFactory rosterBehaviorFactory) =>
            rosterBehaviorFactory.Create(false, false);
    }
}
