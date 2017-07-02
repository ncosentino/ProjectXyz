using System.Collections.Generic;

namespace ProjectXyz.Game.Interface.GameObjects
{
    public interface IGameObjectManager
    {
        IReadOnlyCollection<IGameObject> GameObjects { get; }
    }
}
