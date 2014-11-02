using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentCollectionContract))]
    public interface IEnchantmentCollection : IEnumerable<IEnchantment>
    {
        #region Properties
        int Count { get; }

        IEnchantment this[int index] { get; }
        #endregion

        #region Methods
        bool Contains(IEnchantment enchantment);
        #endregion
    }
}
