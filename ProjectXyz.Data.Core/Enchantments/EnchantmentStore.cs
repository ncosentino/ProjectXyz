using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentStore : IEnchantmentStore
    {
        #region Constructors
        private EnchantmentStore(
            Guid id,
            Guid enchantmentTypeId,
            Guid triggerId,
            Guid statusTypeId,
            Guid enchantmentWeatherId,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentWeatherId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);

            this.Id = id;
            this.EnchantmentTypeId = enchantmentTypeId;
            this.EnchantmentWeatherId = enchantmentWeatherId;
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

        /// <inheritdoc />
        public Guid EnchantmentWeatherId
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        private IEnchantmentStore Create(
            Guid id,
            Guid enchantmentTypeId,
            Guid triggerId,
            Guid statusTypeId,
            Guid enchantmentWeatherId,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentWeatherId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            var enchantmentStore = new EnchantmentStore(
                id,
                enchantmentTypeId,
                triggerId,
                statusTypeId,
                enchantmentWeatherId,
                remainingDuration);
            return enchantmentStore;
        }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(Id != Guid.Empty);
            Contract.Invariant(EnchantmentTypeId != Guid.Empty);
            Contract.Invariant(TriggerId != Guid.Empty);
            Contract.Invariant(StatusTypeId != Guid.Empty);
            Contract.Invariant(EnchantmentWeatherId != Guid.Empty);
            Contract.Invariant(RemainingDuration >= TimeSpan.Zero);
        }
        #endregion
    }
}
