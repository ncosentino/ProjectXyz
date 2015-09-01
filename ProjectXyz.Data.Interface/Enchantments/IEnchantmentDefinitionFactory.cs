using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinitionFactory
    {
        #region Methods
        IEnchantmentDefinition Create(
            Guid id,
            Guid triggerId,
            Guid statusTypeId);
        #endregion
    }
}
