using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentContext
    {
        #region Properties
        Guid ActiveWeatherId { get; }
        #endregion
    }
}
