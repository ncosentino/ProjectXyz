using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemDefinitionRepository
    {
        IEnumerable<IItemDefinition> LoadItemDefinitions(IGeneratorContext generatorContext);
    }
}