using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Interface.GameObjects
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