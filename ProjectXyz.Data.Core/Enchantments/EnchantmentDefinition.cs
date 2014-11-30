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
    public sealed class EnchantmentDefinition : IEnchantmentDefinition
    {
        #region Constructors
        private EnchantmentDefinition(
            Guid id,
            Guid statId,
            Guid calculationId,
            Guid triggerId,
            Guid statusTypeId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration <= maximumDuration);

            this.Id = id;
            this.StatId = statId;
            this.CalculationId = calculationId;
            this.TriggerId = triggerId;
            this.StatusTypeId = statusTypeId;
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;
            this.MinimumDuration = minimumDuration;
            this.MaximumDuration = maximumDuration;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            set;
        }

        public Guid StatId
        {
            get;
            set;
        }

        public Guid CalculationId
        {
            get;
            set;
        }

        public Guid TriggerId
        {
            get;
            set;
        }

        public Guid StatusTypeId
        {
            get;
            set;
        }

        public TimeSpan MinimumDuration
        {
            get;
            set;
        }

        public TimeSpan MaximumDuration
        {
            get;
            set;
        }

        public double MinimumValue
        {
            get;
            set;
        }

        public double MaximumValue
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinition Create(
            Guid id,
            Guid statId,
            Guid calculationId,
            Guid triggerId,
            Guid statusTypeId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration <= maximumDuration);
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            return new EnchantmentDefinition(
                id,
                statId,
                calculationId,
                triggerId,
                statusTypeId,
                minimumValue,
                maximumValue,
                minimumDuration,
                maximumDuration);
        }
        #endregion
    }
}
