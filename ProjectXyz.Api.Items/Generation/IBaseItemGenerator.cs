using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IBaseItemGenerator
    {
        IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext);
    }
}