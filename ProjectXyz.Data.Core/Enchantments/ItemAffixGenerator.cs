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
    public abstract class ItemAffixGenerator
    {
        #region Fields
        private readonly IAffixEnchantmentsRepository _affixEnchantmentsRepository;
        private readonly IEnchantmentRepository _enchantmentRepository;
        #endregion

        #region Constructors
        protected ItemAffixGenerator(
            IAffixEnchantmentsRepository affixEnchantmentsRepository,
            IEnchantmentRepository enchantmentRepository)
        {
            Contract.Requires<ArgumentNullException>(affixEnchantmentsRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentRepository != null);

            _affixEnchantmentsRepository = affixEnchantmentsRepository;
            _enchantmentRepository = enchantmentRepository;
        }
        #endregion

        #region Methods
        protected IEnumerable<IEnchantment> GetEnchantments(Guid affixEnchantmentId, IRandom randomizer)
        {
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            var affixEnchantments = _affixEnchantmentsRepository.GetForId(affixEnchantmentId);
            foreach (var enchantmentId in affixEnchantments.EnchantmentIds)
            {
                yield return _enchantmentRepository.Generate(enchantmentId, randomizer);
            }
        }
        #endregion
    }
}
