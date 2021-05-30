using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api
{
    public interface IReplaceIngredientsCraftingDefinition : IHasFilterAttributes
    {
        IReadOnlyCollection<IIdentifier> DropTableIds { get; }

        IReadOnlyCollection<IReadOnlyCollection<IFilterAttributeValue>> IngredientFilters { get; }
    }
}