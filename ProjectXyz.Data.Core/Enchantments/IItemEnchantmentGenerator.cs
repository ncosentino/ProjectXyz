using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class ItemEnchantmentGenerator : IItemEnchantmentGenerator
    {
        #region Fields
        private readonly IItemAffixRepository _itemAffixRepository;
        private readonly IAffixEnchantmentsRepository _affixEnchantmentsRepository;
        private readonly IEnchantmentRepository _enchantmentRepository;
        #endregion

        #region Constructors
        private ItemEnchantmentGenerator(
            IItemAffixRepository itemAffixRepository,
            IAffixEnchantmentsRepository affixEnchantmentsRepository,
            IEnchantmentRepository enchantmentRepository)
        {
            Contract.Requires<ArgumentNullException>(itemAffixRepository != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentsRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentRepository != null);

            _itemAffixRepository = itemAffixRepository;
            _affixEnchantmentsRepository = affixEnchantmentsRepository;
            _enchantmentRepository = enchantmentRepository;
        }
        #endregion

        #region Methods
        public static IItemEnchantmentGenerator Create(
            IItemAffixRepository itemAffixRepository,
            IAffixEnchantmentsRepository affixEnchantmentRepository,
            IEnchantmentRepository enchantmentsRepository)
        {
            Contract.Requires<ArgumentNullException>(itemAffixRepository != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentsRepository != null);
            Contract.Ensures(Contract.Result<IItemEnchantmentGenerator>() != null);

            return new ItemEnchantmentGenerator(
                itemAffixRepository, 
                affixEnchantmentRepository, 
                enchantmentsRepository);
        }

        public IEnumerable<IEnchantment> GenerateRandomNamed(Guid magicTypeId, int level, Random randomizer, out string prefix, out string suffix)
        {
            suffix = null;
            prefix = null;
            var enchantments = new List<IEnchantment>();

            var generatePrefix = randomizer.NextDouble() >= 0.5;
            if (generatePrefix)
            {
                var affix = _itemAffixRepository.GenerateRandom(true, magicTypeId, level, randomizer);
                prefix = affix.Name;
                enchantments.AddRange(GetEnchantments(affix.AffixEnchantmentsId, randomizer));
            }

            if (!generatePrefix || randomizer.NextDouble() >= 0.5)
            {
                var affix = _itemAffixRepository.GenerateRandom(true, magicTypeId, level, randomizer);
                suffix = affix.Name;
                enchantments.AddRange(GetEnchantments(affix.AffixEnchantmentsId, randomizer));
            }

            return enchantments;
        }

        public IEnumerable<IEnchantment> GenerateRandom(int minimum, int maximum, Guid magicTypeId, int level, Random randomizer)
        {
            foreach (var affix in _itemAffixRepository.GenerateRandom(minimum, maximum, magicTypeId, level, randomizer))
            {
                foreach (var enchantment in GetEnchantments(affix.AffixEnchantmentsId, randomizer))
                {
                    yield return enchantment;
                }
            }
        }

        private IEnumerable<IEnchantment> GetEnchantments(Guid affixEnchantmentId, Random randomizer)
        {
            var affixEnchantments = _affixEnchantmentsRepository.GetForId(affixEnchantmentId);
            foreach (var enchantmentId in affixEnchantments.EnchantmentIds)
            {
                yield return _enchantmentRepository.Generate(enchantmentId, randomizer);
            }
        }
        #endregion
    }
}
