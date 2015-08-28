using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemAffixGenerator : IItemAffixGenerator
    {
        #region Fields
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IItemAffixDefinitionRepository _itemAffixDefinitionRepository;
        private readonly IItemAffixDefinitionFilterRepository _itemAffixDefinitionFilterRepository;
        private readonly IItemAffixEnchantmentRepository _itemAffixEnchantmentRepository;
        private readonly IItemAffixFactory _itemAffixFactory;
        private readonly IMagicTypesRandomAffixesRepository _magicTypesRandomAffixesRepository;
        #endregion

        #region Constructors
        private ItemAffixGenerator(
            IEnchantmentGenerator enchantmentGenerator,
            IItemAffixDefinitionRepository itemAffixDefinitionRepository,
            IItemAffixDefinitionFilterRepository itemAffixDefinitionFilterRepository,
            IItemAffixEnchantmentRepository itemAffixEnchantmentRepository,
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
            IItemAffixEnchantmentRepository itemAffixEnchantmentRepository,
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

            var candidateIds = new List<Guid>(_itemAffixDefinitionFilterRepository.GetIdsForFilter(
                level, 
                int.MaxValue, 
                magicTypeId,
                true,
                true));

            for (int i = 0; i < numberOfAffixes; i++)
            {
                var candidateIndex = (int)(randomizer.NextDouble() * (candidateIds.Count - 1));
                var selectedAffixId = candidateIds[candidateIndex];

                yield return GenerateAffix(randomizer, selectedAffixId);
                candidateIds.RemoveAt(candidateIndex);
            }
        }

        public INamedItemAffix GeneratePrefix(IRandom randomizer, int level, Guid magicTypeId)
        {
            return GenerateNamedAffix(
                randomizer,
                level,
                magicTypeId,
                true);
        }

        public INamedItemAffix GenerateSuffix(IRandom randomizer, int level, Guid magicTypeId)
        {
            return GenerateNamedAffix(
                randomizer,
                level,
                magicTypeId,
                false);
        }

        private IItemAffix GenerateAffix(IRandom randomizer, Guid affixId)
        {
            var affixDefinition = _itemAffixDefinitionRepository.GetById(affixId);
            var enchantments = GetEnchantments(randomizer, affixDefinition.Id).ToArray(); // TODO: in unit testing, this seems to get enumerated twice... not sure why.
            return _itemAffixFactory.CreateItemAffix(enchantments);
        }

        private INamedItemAffix GenerateNamedAffix(IRandom randomizer, int level, Guid magicTypeId, bool prefix)
        {
            var candidateIds = new List<Guid>(_itemAffixDefinitionFilterRepository.GetIdsForFilter(
                level,
                int.MaxValue,
                magicTypeId,
                prefix,
                !prefix));

                var candidateIndex = (int)(randomizer.NextDouble() * (candidateIds.Count - 1));
                var selectedAffixId = candidateIds[candidateIndex];

                var affixDefinition = _itemAffixDefinitionRepository.GetById(selectedAffixId);
                var enchantments = GetEnchantments(randomizer, affixDefinition.Id);
                return _itemAffixFactory.CreateNamedItemAffix(enchantments, affixDefinition.NameStringResourceId);
        }
        
        private IEnumerable<IEnchantment> GetEnchantments(IRandom randomizer, Guid itemAffixDefinitionId)
        {
            var affixEnchantments = _itemAffixEnchantmentRepository.GetByItemAffixDefinitionId(itemAffixDefinitionId);
            return affixEnchantments
                .Select(x => _enchantmentGenerator.Generate(randomizer, x.EnchantmentId));
        }
        #endregion
    }
}
