using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemAffixGenerator : IItemAffixGenerator
    {
        #region Fields
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IAffixEnchantmentsRepository _affixEnchantmentsRepository;
        private readonly IItemAffixDefinitionRepository _itemAffixDefinitionRepository;
        private readonly IItemAffixFactory _itemAffixFactory;
        private readonly IMagicTypesRandomAffixesRepository _magicTypesRandomAffixesRepository;
        #endregion

        #region Constructors
        private ItemAffixGenerator(
            IEnchantmentGenerator enchantmentGenerator,
            IAffixEnchantmentsRepository affixEnchantmentsRepository,
            IItemAffixDefinitionRepository itemAffixDefinitionRepository, 
            IItemAffixFactory itemAffixFactory,
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentsRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixFactory != null);
            Contract.Requires<ArgumentNullException>(magicTypesRandomAffixesRepository != null);

            _enchantmentGenerator = enchantmentGenerator;
            _affixEnchantmentsRepository = affixEnchantmentsRepository;
            _itemAffixDefinitionRepository = itemAffixDefinitionRepository;
            _itemAffixFactory = itemAffixFactory;
            _magicTypesRandomAffixesRepository = magicTypesRandomAffixesRepository;
        }
        #endregion

        #region Methods
        public static ItemAffixGenerator Create(
            IEnchantmentGenerator enchantmentGenerator,
            IAffixEnchantmentsRepository affixEnchantmentsRepository,
            IItemAffixDefinitionRepository itemAffixDefinitionRepository,
            IItemAffixFactory itemAffixFactory,
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentsRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(itemAffixFactory != null);
            Contract.Requires<ArgumentNullException>(magicTypesRandomAffixesRepository != null);
            Contract.Ensures(Contract.Result<IItemAffixGenerator>() != null);

            return new ItemAffixGenerator(
                enchantmentGenerator,
                affixEnchantmentsRepository,
                itemAffixDefinitionRepository,
                itemAffixFactory,
                magicTypesRandomAffixesRepository);
        }

        public IEnumerable<IItemAffix> GenerateRandom(IRandom randomizer, int level, Guid magicTypeId)
        {
            var magicTypesAffix = _magicTypesRandomAffixesRepository.GetForMagicTypeId(magicTypeId);
            var numberOfEnchantments = 
                magicTypesAffix.MinimumAffixes + 
                (int)(randomizer.NextDouble() * (magicTypesAffix.MaximumAffixes - magicTypesAffix.MinimumAffixes));

            var candidateIds = new List<Guid>(_itemAffixDefinitionRepository.GetIdsForFilter(
                level, 
                int.MaxValue, 
                magicTypeId,
                true,
                true));

            var merp = new List<IItemAffix>();
            for (int i = 0; i < numberOfEnchantments; i++)
            {
                var candidateIndex = (int)(randomizer.NextDouble() * (candidateIds.Count - 1));
                var selectedAffixId = candidateIds[candidateIndex];

                ////yield return GenerateAffix(randomizer, selectedAffixId);
                merp.Add(GenerateAffix(randomizer, selectedAffixId));
                candidateIds.RemoveAt(candidateIndex);
            }

            return merp;
        }

        public INamedItemAffix GeneratePrefix(IRandom randomizer, int level, Guid magicTypeId)
        {
            throw new NotImplementedException();
        }

        public INamedItemAffix GenerateSuffix(IRandom randomizer, int level, Guid magicTypeId)
        {
            throw new NotImplementedException();
        }

        private IItemAffix GenerateAffix(IRandom randomizer, Guid affixId)
        {
            var affixDefinition = _itemAffixDefinitionRepository.GetById(affixId);
            var enchantments = GetEnchantments(randomizer, affixDefinition.AffixEnchantmentsId);
            return _itemAffixFactory.CreateItemAffix(enchantments);
        }

        private INamedItemAffix GenerateNamedAffix(IRandom randomizer, Guid affixId)
        {
            var affixDefinition = _itemAffixDefinitionRepository.GetById(affixId);
            var enchantments = GetEnchantments(randomizer, affixDefinition.AffixEnchantmentsId);
            return _itemAffixFactory.CreateNamedItemAffix(enchantments, affixDefinition.Name);
        }

        private IEnumerable<IEnchantment> GetEnchantments(IRandom randomizer, Guid affixEnchantmentsId)
        {
            var affixEnchantments = _affixEnchantmentsRepository.GetById(affixEnchantmentsId);

            var merp = new List<IEnchantment>();
            foreach (var enchantmentId in affixEnchantments.EnchantmentIds)
            {
                ////yield return _enchantmentGenerator.Generate(randomizer, enchantmentId);
                merp.Add(_enchantmentGenerator.Generate(randomizer, enchantmentId));
            }

            return merp;
        }
        #endregion
    }
}
