using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default
{
    public sealed class ReplaceIngredientsCraftingDefinition : IReplaceIngredientsCraftingDefinition
    {
        public ReplaceIngredientsCraftingDefinition(
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IReadOnlyCollection<IFilterAttributeValue>> ingredientFilters,
            IEnumerable<IIdentifier> dropTableIds)
        {
            SupportedAttributes = supportedAttributes;
            IngredientFilters = ingredientFilters.ToArray();
            DropTableIds = dropTableIds.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IReadOnlyCollection<IReadOnlyCollection<IFilterAttributeValue>> IngredientFilters { get; }
        
        public IReadOnlyCollection<IIdentifier> DropTableIds { get; }
    }
}
