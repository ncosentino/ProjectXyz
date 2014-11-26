using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IItemEnchantmentGenerator))]
    public abstract class IItemEnchantmentGeneratorContract : IItemEnchantmentGenerator
    {
        #region Methods
        public IEnumerable<IEnchantment> GenerateRandom(int minimum, int maximum, Guid magicTypeId, int level, Random randomizer)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimum >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(minimum <= maximum);
            Contract.Requires<ArgumentOutOfRangeException>(level >= 0);
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return default(IEnumerable<IEnchantment>);
        }

        public IEnumerable<IEnchantment> GenerateRandomNamed(Guid magicTypeId, int level, Random randomizer, out string prefix, out string suffix)
        {
            Contract.Requires<ArgumentOutOfRangeException>(level >= 0);
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            prefix = default(string);
            suffix = default(string);
            return default(IEnumerable<IEnchantment>);
        }
        #endregion
    }
}
