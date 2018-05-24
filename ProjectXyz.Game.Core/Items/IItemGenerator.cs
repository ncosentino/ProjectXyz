using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Core.Items
{
    public interface IItemGenerator
    {
        IEnumerable<IGameObject> GenerateItems(IItemGenerationContext itemGenerationContext);
    }
}