using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantment))]
    public abstract class IEnchantmentContract : IEnchantment
    {
        #region Properties
        public string StatId
        {
            get
            {
                Contract.Requires<ArgumentNullException>(StatId != null);
                Contract.Requires<ArgumentException>(StatId != string.Empty);
                return default(string);
            }
        }

        public double Value
        {
            get { return default(double); }
        }

        public string CalculationId
        {
            get
            {
                Contract.Requires<ArgumentNullException>(CalculationId != null);
                Contract.Requires<ArgumentException>(CalculationId != string.Empty);
                return default(string);
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
        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            Contract.Ensures(
                RemainingDuration >= TimeSpan.Zero ||
                RemainingDuration == TimeSpan.MinValue);
        }
        #endregion
    }
}
