using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(INamedItemAffixGeneratorContract))]
    public interface INamedItemAffixGenerator
    {
        #region Methods
        IEnumerable<IEnchantment> GenerateRandomNamed(Guid magicTypeId, int level, IRandom randomizer, out string prefix, out string suffix);
        #endregion
    }
}
