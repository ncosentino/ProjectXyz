using System.Collections.Generic;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Items;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Game.Core.Items
{
    public sealed class ItemGenerationContext : IItemGenerationContext
    {
        public ItemGenerationContext(IEnumerable<IItemGenerationContextComponent> components)
        {
            Components = new ComponentCollection(components);
        }

        public IComponentCollection Components { get; }
    }
}