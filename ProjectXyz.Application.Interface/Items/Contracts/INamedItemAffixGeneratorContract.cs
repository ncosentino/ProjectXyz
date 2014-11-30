using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(INamedItemAffixGenerator))]
    public abstract class INamedItemAffixGeneratorContract : INamedItemAffixGenerator
    {
        #region Methods
        public INamedItemAffixes GenerateRandomNamedAffixes(IRandom randomizer, int level, Guid magicTypeId)
        {
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Requires<ArgumentOutOfRangeException>(level >= 0);
            Contract.Ensures(Contract.Result<INamedItemAffixes>() != null);

            return default(INamedItemAffixes);
        }
        #endregion
    }
}
