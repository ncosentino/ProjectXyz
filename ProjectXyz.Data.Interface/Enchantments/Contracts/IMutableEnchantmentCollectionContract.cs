using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IMutableEnchantmentCollection))]
    public abstract class IMutableEnchantmentCollectionContract : IMutableEnchantmentCollection
    {
        public void Add(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        public bool Remove(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            return default(bool);
        }

        public void Clear()
        {
        }
        
        public abstract int Count { get; }

        public abstract IEnchantment this[int index] { get; }

        public abstract bool Contains(IEnchantment enchantment);

        public abstract IEnumerator<IEnchantment> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
