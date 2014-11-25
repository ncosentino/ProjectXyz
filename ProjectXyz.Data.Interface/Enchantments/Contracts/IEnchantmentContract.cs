using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
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
                Contract.Ensures(
                    Contract.Result<TimeSpan>() >= TimeSpan.Zero ||
                    Contract.Result<TimeSpan>() == TimeSpan.MinValue);
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
    }
}
