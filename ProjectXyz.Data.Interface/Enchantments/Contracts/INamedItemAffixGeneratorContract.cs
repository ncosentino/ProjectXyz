using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(INamedItemAffixGenerator))]
    public abstract class INamedItemAffixGeneratorContract : INamedItemAffixGenerator
    {
        #region Methods
        public IEnumerable<IEnchantment> GenerateRandomNamed(Guid magicTypeId, int level, IRandom randomizer, out string prefix, out string suffix)
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
