using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class NoneItemDefinitionRepository : IItemDefinitionRepository
    {
        public IEnumerable<IItemDefinition> LoadItemDefinitions(IGeneratorContext generatorContext) =>
            Enumerable.Empty<IItemDefinition>();
    }
}