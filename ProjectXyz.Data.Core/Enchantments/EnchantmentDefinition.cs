using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentDefinition : IEnchantmentDefinition
    {
        #region Constructors
        private EnchantmentDefinition(
            Guid id,
            Guid triggerId,
            Guid statusTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);

            this.Id = id;
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
        #endregion

        #region Methods
        public static IEnchantmentDefinition Create(
            Guid id,
            Guid triggerId,
            Guid statusTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            var enchantmentDefinition = new EnchantmentDefinition(
                id,
                triggerId,
                statusTypeId);
            return enchantmentDefinition;
        }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(Id != Guid.Empty);
            Contract.Invariant(TriggerId != Guid.Empty);
            Contract.Invariant(StatusTypeId != Guid.Empty);
        }
        #endregion
    }
}
