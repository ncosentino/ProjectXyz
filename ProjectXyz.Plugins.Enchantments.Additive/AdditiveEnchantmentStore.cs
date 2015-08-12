using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class AdditiveEnchantmentStore : 
        EnchantmentStore,
        IAdditiveEnchantmentStore
    {
        #region Constructors
        private AdditiveEnchantmentStore(
            Guid id,
            Guid enchantmentTypeId,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration,
            double value)
            : base(id, enchantmentTypeId, triggerId, statusTypeId, remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);

            this.StatId = statId;
            this.Value = value;
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
        #endregion

        #region Methods
        public static IAdditiveEnchantmentStore Create(
            Guid id,
            Guid enchantmentTypeId,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return new AdditiveEnchantmentStore(
                id,
                enchantmentTypeId,
                statId,
                triggerId,
                statusTypeId,
                remainingDuration,
                value);
        }
        #endregion
    }
}
