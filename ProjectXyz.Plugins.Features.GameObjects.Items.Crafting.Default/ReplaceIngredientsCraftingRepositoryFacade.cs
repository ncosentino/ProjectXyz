using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default
{
    public sealed class ReplaceIngredientsCraftingRepositoryFacade : IReplaceIngredientsCraftingRepositoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableReplaceIngredientsCraftingRepository>> _lazyRepositories;

        public ReplaceIngredientsCraftingRepositoryFacade(Lazy<IEnumerable<IDiscoverableReplaceIngredientsCraftingRepository>> repositories)
        {
            _lazyRepositories = new Lazy<IReadOnlyCollection<IDiscoverableReplaceIngredientsCraftingRepository>>(() =>
                repositories.Value.ToArray());
        }

        public IEnumerable<IReplaceIngredientsCraftingDefinition> GetAll() => _lazyRepositories
            .Value
            .SelectMany(x => x.GetAll());
    }
}
