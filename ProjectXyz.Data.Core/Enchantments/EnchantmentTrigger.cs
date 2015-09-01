using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentTrigger : IEnchantmentTrigger
    {
        #region Constructors
        private EnchantmentTrigger(
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
        public static IEnchantmentTrigger Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentTrigger>() != null);

            var enchantmentTrigger = new EnchantmentTrigger(
                id,
                nameStringResourceId);
            return enchantmentTrigger;
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
