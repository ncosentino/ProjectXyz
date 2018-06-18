using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemGenerator : IHasItemGeneratorAttributes
    {
        IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext);
    }
}