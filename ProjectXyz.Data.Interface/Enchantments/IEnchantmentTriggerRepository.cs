using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentTriggerRepositoryContract))]
    public interface IEnchantmentTriggerRepository
    {
        #region Methods
        IEnchantmentTrigger Add(
            Guid id,
            Guid nameStringResourceId);

        IEnchantmentTrigger GetById(Guid id);
        #endregion
    }
}
