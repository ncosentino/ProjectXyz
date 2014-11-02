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
        public void Add(IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
        }

        public void AddRange(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        public void Remove(IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
        }

        public void RemoveRange(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
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
