using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentStoreCollectionContract))]
    public interface IEnchantmentStoreCollection : IEnumerable<IEnchantmentStore>
    {
        #region Properties
        int Count { get; }

        IEnchantmentStore this[int index] { get; }
        #endregion

        #region Methods
        bool Contains(IEnchantmentStore enchantment);
        #endregion
    }
}
