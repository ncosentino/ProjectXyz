using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IRandomItemAffixGenerator))]
    public abstract class IRandomItemAffixGeneratorContract : IRandomItemAffixGenerator
    {
        #region Methods
        public IEnumerable<IEnchantment> GenerateRandomEnchantments(Guid magicTypeId, int level, IRandom randomizer)
        {
            Contract.Requires<ArgumentOutOfRangeException>(level >= 0);
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return default(IEnumerable<IEnchantment>);
        }
        #endregion
    }
}
