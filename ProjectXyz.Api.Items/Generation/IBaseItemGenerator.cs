using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Api.GameObjects.Items.Generation
{
    public interface IBaseItemGenerator
    {
        IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext);
    }
}