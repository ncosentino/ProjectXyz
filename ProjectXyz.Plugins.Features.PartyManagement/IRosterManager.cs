using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.PartyManagement
{
    public interface IRosterManager : IObservableRosterManager
    {
        void AddToRoster(IGameObject actor);

        void RemoveFromRoster(IGameObject actor);

        void ClearRoster();
    }
}
