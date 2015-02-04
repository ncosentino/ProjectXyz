using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemAffix
    {
        #region Properties
        IEnumerable<IEnchantment> Enchantments { get; }
        #endregion
    }
}
