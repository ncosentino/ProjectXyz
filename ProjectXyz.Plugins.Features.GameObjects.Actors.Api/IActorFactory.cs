using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IActorFactory
    {
        IGameObject Create();
    }
}