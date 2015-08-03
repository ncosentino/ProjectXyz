using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IMutableEnchantmentCollection))]
    public abstract class IMutableEnchantmentCollectionContract : IMutableEnchantmentCollection
    {
        #region Events
        public abstract event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
        public abstract int Count { get; }

        public abstract IEnchantment this[int index] { get; }
        #endregion

        #region Methods
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

        public abstract bool Contains(IEnchantment enchantment);

        public abstract IEnumerator<IEnchantment> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
