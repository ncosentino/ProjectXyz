using System;

namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public interface IObservableRosterManager : IReadOnlyRosterManager
    {
        event EventHandler ActivePartyChanged;

        event EventHandler PartyLeaderChanged;

        event EventHandler RosterChanged;
    }
}
