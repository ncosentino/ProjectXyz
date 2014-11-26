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
    public sealed class RandomItemAffixGenerator : ItemAffixGenerator, IRandomItemAffixGenerator
    {
        #region Fields
        private readonly IItemAffixRepository _itemAffixRepository;
        private readonly IMagicTypesRandomAffixesRepository _magicTypesRandomAffixesRepository;
        #endregion

        #region Constructors
        private RandomItemAffixGenerator(
            IItemAffixRepository itemAffixRepository,
            IAffixEnchantmentsRepository affixEnchantmentsRepository,
            IEnchantmentRepository enchantmentRepository,
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository)
            : base(affixEnchantmentsRepository, enchantmentRepository)
        {
            Contract.Requires<ArgumentNullException>(itemAffixRepository != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentsRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(magicTypesRandomAffixesRepository != null);

            _itemAffixRepository = itemAffixRepository;
            _magicTypesRandomAffixesRepository = magicTypesRandomAffixesRepository;
        }
        #endregion

        #region Methods
        public static IRandomItemAffixGenerator Create(
            IItemAffixRepository itemAffixRepository,
            IAffixEnchantmentsRepository affixEnchantmentRepository,
            IEnchantmentRepository enchantmentRepository,
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository)
        {
            Contract.Requires<ArgumentNullException>(itemAffixRepository != null);
            Contract.Requires<ArgumentNullException>(affixEnchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(magicTypesRandomAffixesRepository != null);
            Contract.Ensures(Contract.Result<IRandomItemAffixGenerator>() != null);

            return new RandomItemAffixGenerator(
                itemAffixRepository, 
                affixEnchantmentRepository, 
                enchantmentRepository,
                magicTypesRandomAffixesRepository);
        }

        public IEnumerable<IEnchantment> GenerateRandomEnchantments(Guid magicTypeId, int level, IRandom randomizer)
        {
            var magicTypesAffixes = _magicTypesRandomAffixesRepository.GetForMagicTypeId(magicTypeId);

            foreach (var affix in _itemAffixRepository.GenerateRandom(
                magicTypesAffixes.MinimumAffixes, 
                magicTypesAffixes.MaximumAffixes, 
                magicTypeId, 
                level, 
                randomizer))
            {
                foreach (var enchantment in GetEnchantments(affix.AffixEnchantmentsId, randomizer))
                {
                    yield return enchantment;
                }
            }
        }
        #endregion
    }
}
