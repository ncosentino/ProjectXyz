using System.Collections.Generic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.Default
{
    public sealed class EnchantmentLoader : IEnchantmentLoader
    {
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IEnchantmentIdentifiers _enchantmentIdentifiers;
        private readonly IEnchantmentGeneratorFacade _enchantmentGeneratorFacade;

        public EnchantmentLoader(
            IEnchantmentGeneratorFacade enchantmentGeneratorFacade,
            IFilterContextFactory filterContextFactory,
            IEnchantmentIdentifiers enchantmentIdentifiers)
        {
            _enchantmentGeneratorFacade = enchantmentGeneratorFacade;
            _filterContextFactory = filterContextFactory;
            _enchantmentIdentifiers = enchantmentIdentifiers;
        }

        public IEnumerable<IGameObject> LoadForEnchantmenDefinitionIds(IEnumerable<IIdentifier> enchantmentDefinitionIds)
        {
            var idFilter = new AnyIdentifierCollectionFilterAttributeValue(enchantmentDefinitionIds);
            var filterContext = _filterContextFactory.CreateContext(
                idFilter.Values.Count,
                idFilter.Values.Count,
                new FilterAttribute(
                    _enchantmentIdentifiers.EnchantmentDefinitionId,
                    idFilter,
                    true));
            foreach (var enchantment in _enchantmentGeneratorFacade.GenerateEnchantments(filterContext))
            {
                yield return enchantment;
            }
        }
    }
}