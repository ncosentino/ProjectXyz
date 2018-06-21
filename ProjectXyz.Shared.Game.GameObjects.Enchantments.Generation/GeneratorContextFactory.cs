using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Game.GameObjects.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation
{
    public sealed class GeneratorContextFactory : IGeneratorContextFactory
    {
        private readonly IReadOnlyCollection<IGeneratorContextAttributeProvider> _generatorContextAttributeProviders;

        public GeneratorContextFactory(IEnumerable<IGeneratorContextAttributeProvider> generatorContextAttributeProviders)
        {
            _generatorContextAttributeProviders = generatorContextAttributeProviders.ToArray();
        }

        public IGeneratorContext CreateItemGeneratorContext(
            int minimumCount,
            int maximumCount)
        {
            var attributes = _generatorContextAttributeProviders.SelectMany(x => x.GetAttributes());
            var itemGeneratorContext = new GeneratorContext(
                minimumCount,
                maximumCount,
                attributes);
            return itemGeneratorContext;
        }
    }
}
