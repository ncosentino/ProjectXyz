using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items.Contracts;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IRandomItemAffixGeneratorContract))]
    public interface IRandomItemAffixGenerator
    {
        #region Methods
        IEnumerable<IEnchantment> GenerateRandomEnchantments(IRandom randomizer, int level, Guid magicTypeId);
        #endregion
    }
}
