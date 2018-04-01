using System.Collections.Generic;
using ProjectXyz.Api.Items;

namespace ProjectXyz.Game.Core.Items
{
    public interface IItemGenerationContextFactory
    {
        IItemGenerationContext Create();

        IItemGenerationContext Merge(
            IItemGenerationContext itemGenerationContext,
            IEnumerable<IItemGenerationContextComponent> itemGenerationContextComponents);
    }
}