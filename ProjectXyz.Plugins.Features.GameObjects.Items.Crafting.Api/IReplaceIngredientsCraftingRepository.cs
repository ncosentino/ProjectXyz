using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api
{
    public interface IReplaceIngredientsCraftingRepository
    {
        IEnumerable<IReplaceIngredientsCraftingDefinition> GetAll();
    }
}