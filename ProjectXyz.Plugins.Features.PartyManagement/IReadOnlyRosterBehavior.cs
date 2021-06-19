
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public interface IReadOnlyRosterBehavior : IBehavior
    {
        bool IsActiveParty { get; }

        bool IsPartyLeader { get; }
    }
}
