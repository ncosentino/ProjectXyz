using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Items;
using ProjectXyz.Framework.Extensions.Collections;

namespace ProjectXyz.Game.Core.Items
{
    public sealed class ItemGenerationContextFactory : IItemGenerationContextFactory
    {
        private readonly IReadOnlyCollection<IItemGenerationContextComponentProvider> _itemGenerationContextComponentProviders;

        public ItemGenerationContextFactory(IEnumerable<IItemGenerationContextComponentProvider> itemGenerationContextComponentProviders)
        {
            _itemGenerationContextComponentProviders = itemGenerationContextComponentProviders.ToArray();
        }

        public IItemGenerationContext Create()
        {
            var components = _itemGenerationContextComponentProviders
                .SelectMany(x => x.CreateComponents());
            var itemGenerationContext = new ItemGenerationContext(components);
            return itemGenerationContext;
        }

        public IItemGenerationContext Merge(
            IItemGenerationContext itemGenerationContext,
            IEnumerable<IItemGenerationContextComponent> itemGenerationContextComponents)
        {
            var components = itemGenerationContext
                .Components
                .TakeTypes<IItemGenerationContextComponent>()
                .Concat(itemGenerationContextComponents);
            var mergedItemGenerationContext = new ItemGenerationContext(components);
            return mergedItemGenerationContext;
        }
    }
}