using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IBaseItemGenerator
    {
        IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext);
    }
}