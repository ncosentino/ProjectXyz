using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantment))]
    public abstract class IEnchantmentContract : IEnchantment
    {
        #region Events
        public abstract event EventHandler<EventArgs> Expired;
        #endregion

        #region Properties
        public Guid Id
        {
            get
            {
                return default(Guid);
            }
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

        public IEnumerable<Guid> WeatherIds
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Guid>>() != null);
                return default(IEnumerable<Guid>);
            }
        }
        #endregion

        #region Methods
        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
