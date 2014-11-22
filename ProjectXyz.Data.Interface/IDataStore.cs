using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Contracts;

namespace ProjectXyz.Data.Interface
{
    [ContractClass(typeof(IDataStoreContract))]
    public interface IDataStore
    {
        #region Properties
        IEnchantmentRepository EnchantmentRepository { get; }
        #endregion
    }
}
