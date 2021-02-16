using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IItemDefinitionRepository
    {
        IEnumerable<IItemDefinition> LoadItemDefinitions(IGeneratorContext generatorContext);
    }
}