using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IRandomItemAffixGenerator))]
    public abstract class IRandomItemAffixGeneratorContract : IRandomItemAffixGenerator
    {
        #region Methods
        public IEnumerable<IEnchantment> GenerateRandomEnchantments(IRandom randomizer, int level, Guid magicTypeId)
        {
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Requires<ArgumentOutOfRangeException>(level >= 0);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return default(IEnumerable<IEnchantment>);
        }
        #endregion
    }
}
