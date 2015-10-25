using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentStatus : IEnchantmentStatus
    {
        #region Constructors
        private EnchantmentStatus(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);

            this.Id = id;
            this.NameStringResourceId = nameStringResourceId;
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
        public Guid NameStringResourceId
        {
            get; 
            private set;
        }
        #endregion

        #region Methods
        public static IEnchantmentStatus Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentStatus>() != null);

            var enchantmentStatus = new EnchantmentStatus(
                id,
                nameStringResourceId);
            return enchantmentStatus;
        }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(Id != Guid.Empty);
            Contract.Invariant(NameStringResourceId != Guid.Empty);
        }
        #endregion
    }
}
