using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentStatusRepository))]
    public abstract class IEnchantmentStatusRepositoryContract : IEnchantmentStatusRepository
    {
        #region Methods
        public IEnchantmentStatus Add(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentStatus>() != null);

            return default(IEnchantmentStatus);
        }

        public IEnchantmentStatus GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IEnchantmentStatus>() != null);

            return default(IEnchantmentStatus);
        }
        #endregion
    }
}
