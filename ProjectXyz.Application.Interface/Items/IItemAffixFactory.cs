using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemAffixFactory
    {
        #region Methods
        IItemAffix CreateItemAffix(IEnumerable<IEnchantment> enchantments);

        INamedItemAffix CreateNamedItemAffix(
            IEnumerable<IEnchantment> enchantments, 
            Guid nameStringResourceId);
        #endregion
    }
}
