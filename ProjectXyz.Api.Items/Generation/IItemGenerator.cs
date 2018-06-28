using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IItemGenerator : IHasGeneratorAttributes
    {
        IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext);
    }
}