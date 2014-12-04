using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

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

        public Guid CalculationId
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

        public TimeSpan RemainingDuration
        {
            get
            {
                Contract.Ensures(
                    Contract.Result<TimeSpan>() >= TimeSpan.Zero ||
                    Contract.Result<TimeSpan>() == TimeSpan.MinValue);
                return default(TimeSpan);
            }
        }
        #endregion
    }
}
