using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments.ExtensionMethods;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentStore : IEnchantmentStore
    {
        #region Constructors
        private EnchantmentStore(
            Guid statId, 
            Guid calculationId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration,
            double value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);

            this.StatId = statId;
            this.CalculationId = calculationId;
            this.TriggerId = triggerId;
            this.StatusTypeId = statusTypeId;
            this.RemainingDuration = remainingDuration;
            this.Value = value;
        }
        #endregion

        #region Properties
        public Guid StatId
        {
            get;
            private set;
        }

        public double Value
        {
            get;
            private set;
        }

        public Guid CalculationId
        {
            get;
            private set;
        }

        public Guid TriggerId
        {
            get;
            private set;
        }

        public Guid StatusTypeId
        {
            get;
            private set;
        }

        public TimeSpan RemainingDuration
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IEnchantmentStore Create(
            Guid statId, 
            Guid calculationId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration,
            double value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return new EnchantmentStore(
                statId,
                calculationId,
                triggerId,
                statusTypeId,
                remainingDuration,
                value);
        }
        #endregion
    }
}
