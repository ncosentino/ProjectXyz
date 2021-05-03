﻿using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace ProjectXyz.Plugins.Features.Enchantments.Generation
{
    public sealed class EnchantmentLoader : IEnchantmentLoader
    {
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IEnchantmentIdentifiers _enchantmentIdentifiers;
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IReadOnlyEnchantmentDefinitionRepositoryFacade _enchantmentDefinitionRepository;
        private readonly IGeneratorComponentToBehaviorConverterFacade _filterComponentToBehaviorConverter;

        public EnchantmentLoader(
            IEnchantmentFactory enchantmentFactory,
            IReadOnlyEnchantmentDefinitionRepositoryFacade enchantmentDefinitionRepository,
            IGeneratorComponentToBehaviorConverterFacade filterComponentToBehaviorConverter,
            IFilterContextFactory filterContextFactory,
            IEnchantmentIdentifiers enchantmentIdentifiers)
        {
            _enchantmentFactory = enchantmentFactory;
            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _filterComponentToBehaviorConverter = filterComponentToBehaviorConverter;
            _filterContextFactory = filterContextFactory;
            _enchantmentIdentifiers = enchantmentIdentifiers;
        }

        public IEnumerable<IGameObject> LoadForEnchantmenDefinitionIds(IEnumerable<IIdentifier> enchantmentDefinitionIds)
        {
            // FIXME: improve this to do one lookup with a context that has 
            // the set of identifiers we want to match
            foreach (var enchantmentDefinitionId in enchantmentDefinitionIds)
            {
                var filterContext = _filterContextFactory
                    .CreateFilterContextForAnyAmount(new FilterAttribute(
                        _enchantmentIdentifiers.EnchantmentDefinitionId,
                        new IdentifierFilterAttributeValue(enchantmentDefinitionId),
                        true));
                foreach (var enchantment in Load(filterContext))
                {
                    yield return enchantment;
                }
            }
        }

        public IEnumerable<IGameObject> Load(IFilterContext filterContext)
        {
            foreach (var enchantmentDefinition in _enchantmentDefinitionRepository
                .ReadEnchantmentDefinitions(filterContext))
            {
                var enchantmentBehaviors = _filterComponentToBehaviorConverter.Convert(
                    Enumerable.Empty<IBehavior>(),
                    enchantmentDefinition.GeneratorComponents);
                var enchantment = _enchantmentFactory.Create(enchantmentBehaviors);
                yield return enchantment;
            }
        }
    }
}