using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IAffixEnchantmentsRepository
    {
        #region Methods
        IAffixEnchantments GetById(Guid id);
        #endregion
    }
}
