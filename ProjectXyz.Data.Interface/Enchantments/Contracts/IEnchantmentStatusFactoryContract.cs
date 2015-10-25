
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentStatusFactory))]
    public abstract class IEnchantmentStatusFactoryContract : IEnchantmentStatusFactory
    {
        #region Methods
        public IEnchantmentStatus Create(Guid id, Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentStatus>() != null);

            return default(IEnchantmentStatus);
        }
        #endregion
    }
}
