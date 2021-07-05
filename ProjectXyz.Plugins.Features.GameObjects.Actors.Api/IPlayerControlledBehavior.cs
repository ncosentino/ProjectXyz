using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IPlayerControlledBehavior : IObservablePlayerControlledBehavior
    {
        new bool IsActive { get; set; }
    }
}
