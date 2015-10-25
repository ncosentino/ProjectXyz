using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentStatusRepositoryContract))]
    public interface IEnchantmentStatusRepository
    {
        #region Methods
        IEnchantmentStatus Add(
            Guid id,
            Guid nameStringResourceId);

        IEnchantmentStatus GetById(Guid id);
        #endregion
    }
}
