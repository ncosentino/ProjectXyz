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
        public Guid StatId
        {
            get
            {
                return default(Guid);
            }

            set
            {
            }
        }

        public double Value
        {
            get { return default(double); }

            set { }
        }

        public Guid CalculationId
        {
            get
            {
                return default(Guid);
            }

            set
            {
            }
        }
        
        public Guid TriggerId
        {
            get
            {
                return default(Guid);
            }

            set
            {
            }
        }

        public Guid StatusTypeId
        {
            get
            {
                return default(Guid);
            }

            set
            {
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

            set
            {
                Contract.Requires(
                    value >= TimeSpan.Zero ||
                    value == TimeSpan.MinValue);
            }
        }
        #endregion

        #region Methods
        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
