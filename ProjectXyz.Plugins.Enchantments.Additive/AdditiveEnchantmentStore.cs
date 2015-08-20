using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class AdditiveEnchantmentStore : IAdditiveEnchantmentStore
    {
        #region Constructors
        private AdditiveEnchantmentStore(
            Guid id,
            Guid statId,
            double value,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);

            this.Id = id;
            this.StatId = statId;
            this.Value = value;
            this.RemainingDuration = remainingDuration;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid StatId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public double Value
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid Id
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public TimeSpan RemainingDuration
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentStore Create(
            Guid id,
            Guid statId,
            double value,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStore>() != null);

            return new AdditiveEnchantmentStore(
                id,
                statId,
                value,
                remainingDuration);
        }
        #endregion
    }
}
