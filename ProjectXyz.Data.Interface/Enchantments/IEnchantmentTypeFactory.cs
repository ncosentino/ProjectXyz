using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentTypeFactory
    {
        #region Methods
        IEnchantmentType Create(
            Guid id,
            Guid nameStringResourceId);
        #endregion
    }
}
