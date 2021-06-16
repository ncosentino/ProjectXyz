using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Systems
{
    public interface ISystem
    {
        Task UpdateAsync(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects);
    }
}