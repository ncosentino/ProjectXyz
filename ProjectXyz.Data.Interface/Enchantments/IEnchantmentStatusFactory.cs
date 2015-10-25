using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentStatusFactoryContract))]
    public interface IEnchantmentStatusFactory
    {
        #region Methods
        IEnchantmentStatus Create(Guid id, Guid nameStringResourceId);
        #endregion
    }
}
