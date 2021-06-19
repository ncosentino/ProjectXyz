namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public static class IRosterManagerExtensions
    {
        public static bool ExistsInActiveParty(
            this IReadOnlyRosterManager rosterManager,
            IReadOnlyRosterBehavior rosterBehavior) =>
            rosterManager.ExistsInRoster(rosterBehavior) &&
            rosterBehavior.IsActiveParty;

        public static bool ExistsInRoster(
            this IReadOnlyRosterManager rosterManager,
            IReadOnlyRosterBehavior rosterBehavior) =>
            rosterManager.ExistsInRoster(rosterBehavior.Owner);
    }
}
