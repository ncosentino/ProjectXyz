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
        public string StatId
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
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
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
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

        public string StatusType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
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
