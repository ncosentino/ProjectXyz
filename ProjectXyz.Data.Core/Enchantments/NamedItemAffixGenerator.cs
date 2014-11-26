using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class NamedItemAffixGenerator : ItemAffixGenerator, INamedItemAffixGenerator
    {
        #region Fields
        private readonly IItemAffixRepository _itemAffixRepository;
        #endregion

        #region Constructors
        private NamedItemAffixGenerator(
            IItemAffixRepository itemAffixRepository,
            IAffixEnchantmentsRepository affixEnchantmentsRepository,
            IEnchantmentRepository enchantmentRepository)
            : base(affixEnchantmentsRepository, enchantmentRepository)
        {
            Contract.Requires<ArgumentNullException>(itemAffixRepository != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentsRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentRepository != null);

            _itemAffixRepository = itemAffixRepository;
        }
        #endregion

        #region Methods
        public static INamedItemAffixGenerator Create(
            IItemAffixRepository itemAffixRepository,
            IAffixEnchantmentsRepository affixEnchantmentRepository,
            IEnchantmentRepository enchantmentsRepository)
        {
            Contract.Requires<ArgumentNullException>(itemAffixRepository != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentsRepository != null);
            Contract.Ensures(Contract.Result<INamedItemAffixGenerator>() != null);

            return new NamedItemAffixGenerator(
                itemAffixRepository, 
                affixEnchantmentRepository, 
                enchantmentsRepository);
        }

        public IEnumerable<IEnchantment> GenerateRandomNamed(Guid magicTypeId, int level, IRandom randomizer, out string prefix, out string suffix)
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
                var affix = _itemAffixRepository.GenerateRandom(false, magicTypeId, level, randomizer);
                suffix = affix.Name;
                enchantments.AddRange(GetEnchantments(affix.AffixEnchantmentsId, randomizer));
            }

            return enchantments;
        }
        #endregion
    }
}
