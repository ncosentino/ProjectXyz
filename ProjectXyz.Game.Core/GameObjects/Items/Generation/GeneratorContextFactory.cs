using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Game.GameObjects.Generation;

namespace ProjectXyz.Game.Core.GameObjects.Items.Generation
{
    public sealed class GeneratorContextFactory : IGeneratorContextFactory
    {
        private readonly IReadOnlyCollection<IGeneratorContextAttributeProvider> _itemGeneratorContextAttributeProviders;

        public GeneratorContextFactory(IEnumerable<IGeneratorContextAttributeProvider> itemGeneratorContextAttributeProviders)
        {
            _itemGeneratorContextAttributeProviders = itemGeneratorContextAttributeProviders.ToArray();
        }

        public IGeneratorContext CreateItemGeneratorContext(
            int minimumCount,
            int maximumCount)
        {
            var attributes = _itemGeneratorContextAttributeProviders.SelectMany(x => x.GetAttributes());
            var itemGeneratorContext = new GeneratorContext(
                minimumCount,
                maximumCount,
                attributes);
            return itemGeneratorContext;
        }
    }
}
