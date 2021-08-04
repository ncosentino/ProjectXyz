using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapGameObjectManager : IReadOnlyMapGameObjectManager
    {
        void MarkForRemoval(params IGameObject[] gameObjects);

        void MarkForRemoval(IEnumerable<IGameObject> gameObjects);

        void MarkForAddition(params IGameObject[] gameObjects);

        void MarkForAddition(IEnumerable<IGameObject> gameObjects);

        Task SynchronizeAsync();

        Task ClearGameObjectsImmediateAsync();
    }
}