using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IItemEnchantmentGenerator
    {
        #region Methods
        IEnumerable<IEnchantment> GenerateRandom(int minimum, int maximum, Guid magicTypeId, int level, Random randomizer);

        IEnumerable<IEnchantment> GenerateRandomNamed(Guid magicTypeId, int level, Random randomizer, out string prefix, out string suffix);
        #endregion
    }
}
