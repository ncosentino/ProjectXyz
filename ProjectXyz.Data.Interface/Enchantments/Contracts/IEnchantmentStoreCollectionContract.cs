using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentStoreCollection))]
    public abstract class IEnchantmentStoreCollectionContract : IEnchantmentStoreCollection
    {
        #region Properties
        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public IEnchantmentStore this[int index]
        {
            get
            {
                Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
                Contract.Requires<ArgumentOutOfRangeException>(index < Count);
                Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);
                return default(IEnchantmentStore);
            }
        }

        public bool Contains(IEnchantmentStore enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            return default(bool);
        }

        public IEnumerator<IEnchantmentStore> GetEnumerator()
        {
            return default(IEnumerator<IEnchantmentStore>);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return default(System.Collections.IEnumerator);
        }
        #endregion
    }
}
