using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemAffixFactory
    {
        #region Methods
        IItemAffix CreateItemAffix(IEnumerable<IEnchantment> enchantments);

        INamedItemAffix CreateNamedItemAffix(IEnumerable<IEnchantment> enchantments, string name);
        #endregion
    }
}
