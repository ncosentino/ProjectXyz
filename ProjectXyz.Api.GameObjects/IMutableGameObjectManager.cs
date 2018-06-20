using System.Collections.Generic;

namespace ProjectXyz.Api.GameObjects
{
    public interface IMutableGameObjectManager : IGameObjectManager
    {
        void MarkForRemoval(params IGameObject[] gameObjects);

        void MarkForRemoval(IEnumerable<IGameObject> gameObjects);

        void MarkForAddition(params IGameObject[] gameObjects);

        void MarkForAddition(IEnumerable<IGameObject> gameObjects);

        void Synchronize();
    }
}