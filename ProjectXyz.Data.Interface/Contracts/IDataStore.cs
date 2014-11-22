﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Interface.Contracts
{
    [ContractClassFor(typeof(IDataStore))]
    public abstract class IDataStoreContract : IDataStore
    {
        #region Properties
        public IEnchantmentRepository EnchantmentRepository
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnchantmentRepository>() != null);
                return default(IEnchantmentRepository);
            }
        }
        #endregion
    }
}
