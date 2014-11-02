using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IReadonlyEnchantmentCollection))]
    public abstract class IReadonlyEnchantmentCollectionContract : IReadonlyEnchantmentCollection
    {        
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
