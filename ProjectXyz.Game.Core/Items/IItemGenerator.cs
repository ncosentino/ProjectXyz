using System.Collections.Generic;
using ProjectXyz.Game.Interface.GameObjects;

namespace ProjectXyz.Game.Core.Items
{
    public interface IItemGenerator
    {
        IEnumerable<IGameObject> GenerateItems(IItemGenerationContext itemGenerationContext);
    }
}