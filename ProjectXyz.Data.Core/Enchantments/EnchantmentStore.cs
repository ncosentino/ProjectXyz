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
            Guid enchantmentWeatherId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentWeatherId != Guid.Empty);

            this.Id = id;
            this.EnchantmentTypeId = enchantmentTypeId;
            this.WeatherGroupingId = enchantmentWeatherId;
            this.TriggerId = triggerId;
            this.StatusTypeId = statusTypeId;
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
        public Guid EnchantmentTypeId
        {
            get; 
            private set;
        }

        /// <inheritdoc />
        public Guid WeatherGroupingId
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IEnchantmentStore Create(
            Guid id,
            Guid enchantmentTypeId,
            Guid triggerId,
            Guid statusTypeId,
            Guid weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            var enchantmentStore = new EnchantmentStore(
                id,
                enchantmentTypeId,
                triggerId,
                statusTypeId,
                weatherTypeGroupingId);
            return enchantmentStore;
        }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(Id != Guid.Empty);
            Contract.Invariant(EnchantmentTypeId != Guid.Empty);
            Contract.Invariant(TriggerId != Guid.Empty);
            Contract.Invariant(StatusTypeId != Guid.Empty);
            Contract.Invariant(WeatherGroupingId != Guid.Empty);
        }
        #endregion
    }
}
