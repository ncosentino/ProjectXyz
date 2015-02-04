using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IMutableEnchantmentCollectionContract))]
    public interface IMutableEnchantmentCollection : IEnchantmentStoreCollection
    {
        #region Methods
        void Add(IEnumerable<IEnchantmentStore> enchantments);

        bool Remove(IEnumerable<IEnchantmentStore> enchantments);

        void Clear();
        #endregion
    }
}
