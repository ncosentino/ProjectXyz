using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Interface
{
    public interface IDataStore
    {
        #region Properties
        IEnchantmentRepository EnchantmentRepository { get; }
        #endregion
    }
}
