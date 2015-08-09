using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public abstract class EnchantmentStore : IEnchantmentStore
    {
        #region Constructors
        protected EnchantmentStore(
            Guid id,
            Guid enchantmentTypeId,
            Guid triggerId,
            Guid statusTypeId,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);

            this.Id = id;
            this.EnchantmentTypeId = enchantmentTypeId;
            this.TriggerId = triggerId;
            this.StatusTypeId = statusTypeId;
            this.RemainingDuration = remainingDuration;
        }
        #endregion

        #region Properties        
        /// <inheritdoc />
        public Guid Id
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid TriggerId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid StatusTypeId
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

        /// <inheritdoc />
        public Guid EnchantmentTypeId
        {
            get; 
            private set;
        }
        #endregion
    }
}
