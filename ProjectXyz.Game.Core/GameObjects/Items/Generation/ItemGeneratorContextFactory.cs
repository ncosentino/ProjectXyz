using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;
using ProjectXyz.Shared.Game.Items.Generation;

namespace ProjectXyz.Game.Core.GameObjects.Items.Generation
{
    public sealed class ItemGeneratorContextFactory : IItemGeneratorContextFactory
    {
        private readonly IReadOnlyCollection<IItemGeneratorContextAttributeProvider> _itemGeneratorContextAttributeProviders;

        public ItemGeneratorContextFactory(IEnumerable<IItemGeneratorContextAttributeProvider> itemGeneratorContextAttributeProviders)
        {
            _itemGeneratorContextAttributeProviders = itemGeneratorContextAttributeProviders.ToArray();
        }

        public IItemGeneratorContext CreateItemGeneratorContext(
            int minimumCount,
            int maximumCount)
        {
            var attributes = _itemGeneratorContextAttributeProviders.SelectMany(x => x.GetAttributes());
            var itemGeneratorContext = new ItemGeneratorContext(
                minimumCount,
                maximumCount,
                attributes);
            return itemGeneratorContext;
        }
    }
}
