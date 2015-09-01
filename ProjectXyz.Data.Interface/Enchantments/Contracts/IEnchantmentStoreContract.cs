using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentStore))]
    public abstract class IEnchantmentStoreContract : IEnchantmentStore
    {
        #region Properties
        public Guid Id
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);

                return default(Guid);
            }
        }
        
        public Guid StatId
        {
            get
            {
                return default(Guid);
            }
        }

        public double Value
        {
            get { return default(double); }
        }
        
        public Guid TriggerId
        {
            get
            {
                return default(Guid);
            }
        }

        public Guid StatusTypeId
        {
            get
            {
                return default(Guid);
            }
        }

        public Guid EnchantmentTypeId
        {
            get
            {
                return default(Guid);
            }
        }

        public Guid WeatherGroupingId
        {
            get
            {
                return default(Guid);
            }
        }
        #endregion
    }
}
