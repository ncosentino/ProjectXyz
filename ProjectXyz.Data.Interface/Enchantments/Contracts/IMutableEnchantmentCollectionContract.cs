using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IMutableEnchantmentCollection))]
    public abstract class IMutableEnchantmentCollectionContract : IMutableEnchantmentCollection
    {
        public void Add(IEnumerable<IEnchantmentStore> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        public bool Remove(IEnumerable<IEnchantmentStore> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            return default(bool);
        }

        public void Clear()
        {
        }
        
        public abstract int Count { get; }

        public abstract IEnchantmentStore this[int index] { get; }

        public abstract bool Contains(IEnchantmentStore enchantment);

        public abstract IEnumerator<IEnchantmentStore> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
