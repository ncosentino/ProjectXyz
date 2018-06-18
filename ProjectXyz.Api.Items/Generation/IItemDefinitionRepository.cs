using System.Collections.Generic;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemDefinitionRepository
    {
        IEnumerable<IItemDefinition> LoadItemDefinitions(IItemGeneratorContext itemGeneratorContext);
    }
}