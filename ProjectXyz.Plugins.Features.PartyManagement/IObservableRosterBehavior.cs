using System;

namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public interface IObservableRosterBehavior : IReadOnlyRosterBehavior
    {
        event EventHandler IsActivePartyChanged;

        event EventHandler IsPartyLeaderChanged;
    }
}
