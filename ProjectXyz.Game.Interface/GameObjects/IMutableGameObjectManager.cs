using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Interface.GameObjects
{
    public interface IMutableGameObjectManager : IGameObjectManager
    {
        void MarkForRemoval(IGameObject gameObject);

        void MarkForAddition(IGameObject gameObject);

        void Synchronize();
    }
}