using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.Default
{
    public sealed class EnchantmentGeneratorFacade : IEnchantmentGeneratorFacade
    {
        private readonly Lazy<IReadOnlyCollection<IEnchantmentGenerator>> _lazyEnchantmentGenerators;

        public EnchantmentGeneratorFacade(
            Lazy<IEnumerable<IDiscoverableEnchantmentGenerator>> lazyDiscoverableEnchantmentGenerators)
        {
            _lazyEnchantmentGenerators = new Lazy<IReadOnlyCollection<IEnchantmentGenerator>>(() =>
                lazyDiscoverableEnchantmentGenerators.Value.ToArray());
        }

        public IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext)
        {
            if (!_lazyEnchantmentGenerators.Value.Any())
            {
                throw new InvalidOperationException(
                    $"There are no enchantment generators registered to this " +
                    $"facade. Did you forget to register your type of " +
                    $"'{typeof(IDiscoverableEnchantmentGenerator)}'?");
            }

            var generatedEnchantments = _lazyEnchantmentGenerators
                .Value
                .SelectMany(generator => generator.GenerateEnchantments(filterContext));
            return generatedEnchantments;
        }
    }
}