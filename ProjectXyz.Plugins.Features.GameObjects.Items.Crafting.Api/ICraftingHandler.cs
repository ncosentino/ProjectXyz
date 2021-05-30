using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api
{
    public interface ICraftingHandler
    {
        bool TryHandle(
            IReadOnlyCollection<IFilterAttribute> filterAttributes,
            IReadOnlyCollection<IGameObject> ingredients,
            out IReadOnlyCollection<IGameObject> newItems);
    }
}