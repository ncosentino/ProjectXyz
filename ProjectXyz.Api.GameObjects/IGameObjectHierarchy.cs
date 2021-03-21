using System.Collections.Generic;

namespace ProjectXyz.Api.GameObjects
{
    public interface IGameObjectHierarchy
    {
        IEnumerable<IGameObject> GetChildren(
            IGameObject parent,
            bool recursive);
    }
}