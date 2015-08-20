using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class AdditiveEnchantmentDefinition : IAdditiveEnchantmentDefinition
    {
        #region Constructors
        private AdditiveEnchantmentDefinition(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(maximumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration <= maximumDuration);

            this.Id = id;
            this.StatId = statId;
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;
            this.MinimumDuration = minimumDuration;
            this.MaximumDuration = maximumDuration;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id
        {
            get;
            set;
        }

        /// <inheritdoc />
        public Guid StatId
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double MinimumValue
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double MaximumValue
        {
            get;
            set;
        }

        /// <inheritdoc />
        public TimeSpan MinimumDuration
        {
            get;
            set;
        }

        /// <inheritdoc />
        public TimeSpan MaximumDuration
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentDefinition Create(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(maximumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration <= maximumDuration);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentDefinition>() != null);

            var enchantmentDefinition = new AdditiveEnchantmentDefinition(
                id,
                statId,
                minimumValue,
                maximumValue,
                minimumDuration,
                maximumDuration);
            return enchantmentDefinition;
        }
        #endregion
    }
}
