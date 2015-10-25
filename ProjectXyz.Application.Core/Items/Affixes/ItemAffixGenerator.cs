using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Application.Core.Items.Affixes
{
    public sealed class ItemAffixGenerator : IItemAffixGenerator
    {
        #region Fields
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IItemAffixDefinitionRepository _itemAffixDefinitionRepository;
        private readonly IItemAffixDefinitionFilterRepository _itemAffixDefinitionFilterRepository;
        private readonly IItemAffixDefinitionEnchantmentRepository _itemAffixEnchantmentRepository;
        private readonly IItemAffixFactory _itemAffixFactory;
        private readonly IMagicTypesRandomAffixesRepository _magicTypesRandomAffixesRepository;
        #endregion

        #region Constructors
        private ItemAffixGenerator(
            IEnchantmentGenerator enchantmentGenerator,
            IItemAffixDefinitionRepository itemAffixDefinitionRepository,
            IItemAffixDefinitionFilterRepository itemAffixDefinitionFilterRepository,
            IItemAffixDefinitionEnchantmentRepository itemAffixEnchantmentRepository,
            IItemAffixFactory itemAffixFactory,
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Requires<ArgumentNullException>(itemAffixDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixDefinitionFilterRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixEnchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixFactory != null);
            Contract.Requires<ArgumentNullException>(magicTypesRandomAffixesRepository != null);

            _enchantmentGenerator = enchantmentGenerator;
            _itemAffixDefinitionRepository = itemAffixDefinitionRepository;
            _itemAffixDefinitionFilterRepository = itemAffixDefinitionFilterRepository;
            _itemAffixEnchantmentRepository = itemAffixEnchantmentRepository;
            _itemAffixFactory = itemAffixFactory;
            _magicTypesRandomAffixesRepository = magicTypesRandomAffixesRepository;
        }
        #endregion

        #region Methods
        public static ItemAffixGenerator Create(
            IEnchantmentGenerator enchantmentGenerator,
            IItemAffixDefinitionRepository itemAffixDefinitionRepository,
            IItemAffixDefinitionFilterRepository itemAffixDefinitionFilterRepository,
            IItemAffixDefinitionEnchantmentRepository itemAffixEnchantmentRepository,
            IItemAffixFactory itemAffixFactory,
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Requires<ArgumentNullException>(itemAffixDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixDefinitionFilterRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixEnchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixFactory != null);
            Contract.Requires<ArgumentNullException>(magicTypesRandomAffixesRepository != null);
            Contract.Ensures(Contract.Result<IItemAffixGenerator>() != null);

            return new ItemAffixGenerator(
                enchantmentGenerator,
                itemAffixDefinitionRepository,
                itemAffixDefinitionFilterRepository,
                itemAffixEnchantmentRepository,
                itemAffixFactory,
                magicTypesRandomAffixesRepository);
        }

        public IEnumerable<IItemAffix> GenerateRandom(IRandom randomizer, int level, Guid magicTypeId)
        {
            var magicTypesAffix = _magicTypesRandomAffixesRepository.GetForMagicTypeId(magicTypeId);
            var minimumAffixes = magicTypesAffix.MinimumAffixes;
            var numberOfAffixes =
                minimumAffixes +
                (int)(randomizer.NextDouble() * (magicTypesAffix.MaximumAffixes - minimumAffixes));

            var candidateIds = _itemAffixDefinitionFilterRepository.GetIdsForFilter(
                level, 
                int.MaxValue, 
                magicTypeId,
                true,
                true)
                .ToList();
            if (candidateIds.Count < 1)
            {
                throw new InvalidOperationException("No item affixes exist for the specified filter.");
            }

            for (int i = 0; i < numberOfAffixes; i++)
            {
                var candidateIndex = (int)(randomizer.NextDouble() * (candidateIds.Count - 1));
                var selectedAffixId = candidateIds[candidateIndex];

                yield return GenerateAffix(randomizer, selectedAffixId);
                candidateIds.RemoveAt(candidateIndex);
            }
        }

        public IItemAffix GeneratePrefix(IRandom randomizer, int level, Guid magicTypeId)
        {
            return GenerateNamedAffix(
                randomizer,
                level,
                magicTypeId,
                true);
        }

        public IItemAffix GenerateSuffix(IRandom randomizer, int level, Guid magicTypeId)
        {
            return GenerateNamedAffix(
                randomizer,
                level,
                magicTypeId,
                false);
        }

        private IItemAffix GenerateAffix(IRandom randomizer, Guid affixDefinitionId)
        {
            var affixDefinition = _itemAffixDefinitionRepository.GetById(affixDefinitionId);
            var enchantments = GetEnchantments(randomizer, affixDefinition.Id).ToArray(); // TODO: in unit testing, this seems to get enumerated twice... not sure why.
            return _itemAffixFactory.Create(
                affixDefinitionId,
                affixDefinition.NameStringResourceId,
                enchantments);
        }

        private IItemAffix GenerateNamedAffix(IRandom randomizer, int level, Guid magicTypeId, bool prefix)
        {
            var candidateIds = _itemAffixDefinitionFilterRepository.GetIdsForFilter(
                level,
                int.MaxValue,
                magicTypeId,
                prefix,
                !prefix)
                .ToArray();
            if (candidateIds.Length < 1)
            {
                throw new InvalidOperationException("No item affixes exist for the specified filter.");
            }

            var candidateIndex = (int)(randomizer.NextDouble() * (candidateIds.Length - 1));
            var selectedAffixDefinitionId = candidateIds[candidateIndex];

            var affix = GenerateAffix(
                randomizer,
                selectedAffixDefinitionId);
            return affix;
        }

        private IEnumerable<IEnchantment> GetEnchantments(IRandom randomizer, Guid itemAffixDefinitionId)
        {
            var affixEnchantments = _itemAffixEnchantmentRepository.GetByItemAffixDefinitionId(itemAffixDefinitionId);
            return affixEnchantments
                .Select(x => _enchantmentGenerator.Generate(randomizer, x.EnchantmentDefinitionId));
        }
        #endregion
    }
}
