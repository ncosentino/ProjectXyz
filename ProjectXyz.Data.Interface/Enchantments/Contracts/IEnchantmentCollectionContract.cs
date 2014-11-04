using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentCollection))]
    public abstract class IEnchantmentCollectionContract : IEnchantmentCollection
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

        public IEnchantment this[int index]
        {
            get
            {
                Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
                Contract.Requires<ArgumentOutOfRangeException>(index < Count);
                Contract.Ensures(Contract.Result<IEnchantment>() != null);
                return default(IEnchantment);
            }
        }

        public bool Contains(IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            return default(bool);
        }

        public IEnumerator<IEnchantment> GetEnumerator()
        {
            return default(IEnumerator<IEnchantment>);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return default(System.Collections.IEnumerator);
        }
        #endregion
    }
}
