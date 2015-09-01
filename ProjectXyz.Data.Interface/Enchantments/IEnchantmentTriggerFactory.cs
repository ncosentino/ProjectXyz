using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentTriggerFactoryContract))]
    public interface IEnchantmentTriggerFactory
    {
        #region Methods
        IEnchantmentTrigger Create(Guid id, Guid nameStringResourceId);
        #endregion
    }
}
