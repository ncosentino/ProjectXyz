using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IItemEnchantmentGeneratorContract))]
    public interface IItemEnchantmentGenerator
    {
        #region Methods
        IEnumerable<IEnchantment> GenerateRandom(int minimum, int maximum, Guid magicTypeId, int level, Random randomizer);

        IEnumerable<IEnchantment> GenerateRandomNamed(Guid magicTypeId, int level, Random randomizer, out string prefix, out string suffix);
        #endregion
    }
}
