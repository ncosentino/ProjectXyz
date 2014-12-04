using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantment))]
    public abstract class IEnchantmentContract : IEnchantment
    {
        #region Properties
        public Guid Id
        {
            get
            {
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
                Contract.Requires(
                    RemainingDuration >= TimeSpan.Zero ||
                    RemainingDuration == TimeSpan.MinValue);
                return default(TimeSpan);
            }
        }
        #endregion

        #region Methods
        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
