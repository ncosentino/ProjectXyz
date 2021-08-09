using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public interface IReadOnlyRosterManager
    {
        IReadOnlyCollection<IGameObject> ActiveParty { get; }

        IGameObject ActiveControlledActor { get; }

        IGameObject ActivePartyLeader { get; }

        IReadOnlyCollection<IGameObject> FullRoster { get; }

        bool ExistsInActiveParty(IGameObject actor);

        bool ExistsInRoster(IGameObject actor);
    }
}
