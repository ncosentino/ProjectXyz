using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default
{
    public sealed class InMemoryReplaceIngredientsCraftingRepository : IDiscoverableReplaceIngredientsCraftingRepository
    {
        private readonly IReadOnlyCollection<IReplaceIngredientsCraftingDefinition> _definitions;

        public InMemoryReplaceIngredientsCraftingRepository(IEnumerable<IReplaceIngredientsCraftingDefinition> definitions)
        {
            _definitions = definitions.ToArray();
        }

        public IEnumerable<IReplaceIngredientsCraftingDefinition> GetAll() => _definitions;
    }
}
