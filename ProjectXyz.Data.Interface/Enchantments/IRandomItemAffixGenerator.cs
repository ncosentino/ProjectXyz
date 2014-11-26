using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IRandomItemAffixGeneratorContract))]
    public interface IRandomItemAffixGenerator
    {
        #region Methods
        IEnumerable<IEnchantment> GenerateRandomEnchantments(Guid magicTypeId, int level, IRandom randomizer);
        #endregion
    }
}
