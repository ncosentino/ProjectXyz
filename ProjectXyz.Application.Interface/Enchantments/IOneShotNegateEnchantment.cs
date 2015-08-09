using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IOneShotNegateEnchantment : IEnchantment
    {
        #region Properties
        Guid StatId { get; }
        #endregion
    }
}
