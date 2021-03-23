using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface ICombatGameObjectProvider
    {
        IEnumerable<IGameObject> GetGameObjects();
    }
}
