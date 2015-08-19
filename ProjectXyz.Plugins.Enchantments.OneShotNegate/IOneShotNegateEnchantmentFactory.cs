using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public interface IOneShotNegateEnchantmentFactory
    {
        #region Methods
        IOneShotNegateEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            IEnumerable<Guid> weatherIds,
            Guid statId);
        #endregion
    }
}
