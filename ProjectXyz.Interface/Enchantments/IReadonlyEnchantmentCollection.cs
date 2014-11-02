using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Enchantments.Contracts;

namespace ProjectXyz.Interface.Enchantments
{
    [ContractClass(typeof(IReadonlyEnchantmentCollectionContract))]
    public interface IReadonlyEnchantmentCollection : IEnchantmentCollection
    {
    }
}
