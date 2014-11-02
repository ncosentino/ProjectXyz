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

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
            }
        }

        public double Value
        {
            get { return default(double); }

            set { }
        }

        public string CalculationId
        {
            get
            {
                Contract.Requires<ArgumentNullException>(CalculationId != null);
                Contract.Requires<ArgumentException>(CalculationId != string.Empty);
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
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
    }
}
